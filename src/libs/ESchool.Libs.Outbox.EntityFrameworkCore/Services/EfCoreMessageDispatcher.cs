using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Outbox.EntityFrameworkCore.Entities;
using ESchool.Libs.Outbox.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MoreLinq;

namespace ESchool.Libs.Outbox.EntityFrameworkCore.Services
{
    public class EfCoreMessageDispatcher<TContext> : IMessageDispatcher
        where TContext : DbContext, IOutboxDbContext
    {
        private readonly TContext context;
        private readonly IOptions<OutboxConfiguration> options;
        private readonly IPublishEndpoint publishEndpoint;
        private readonly ILogger<EfCoreMessageDispatcher<TContext>> logger;
        private readonly string schemaQualifiedTableName;

        public EfCoreMessageDispatcher(
            TContext context,
            IOptions<OutboxConfiguration> options,
            IPublishEndpoint publishEndpoint,
            ILogger<EfCoreMessageDispatcher<TContext>> logger)
        {
            this.context = context;
            this.options = options;
            this.publishEndpoint = publishEndpoint;
            this.logger = logger;
            schemaQualifiedTableName = context.Model.FindEntityType(typeof(OutboxEntry))
                .GetSchemaQualifiedTableName();
        }

        public async Task DispatchAllAsync(CancellationToken cancellationToken = default)
        {
            var messageIds = await context.OutboxEntries.Where(x => x.State == OutboxEntryState.Pending)
                .OrderBy(x => x.CreatedAt)
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);

            if (messageIds.Count > 0)
            {
                await TryDispatchMessagesAsync(messageIds, cancellationToken);
            }
        }

        public async Task TryDispatchMessagesAsync(IEnumerable<Guid> messageIds,
            CancellationToken cancellationToken = default)
        {
            var ids = messageIds.ToList();
            foreach (var id in ids)
            {
                await DispatchMessageAsync(id, cancellationToken);
            }
            logger.LogDebug("Successfully dispatched {0} messages", ids.Count);
        }

        private async Task DispatchMessageAsync(Guid messageId, CancellationToken cancellationToken)
        {
            await using var transaction = await context.Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);

            try
            {
                await context.Database.ExecuteSqlRawAsync($"LOCK TABLE \"{schemaQualifiedTableName}\" IN ACCESS EXCLUSIVE MODE", cancellationToken);

                // The filtering must be done in the raw sql query so that the whole table is not gonna get locked.
                var entry = await context.OutboxEntries
                    .FromSqlRaw($"SELECT * " +
                                $"FROM \"{schemaQualifiedTableName}\" " +
                                $"WHERE \"{nameof(OutboxEntry.Id)}\" = '{messageId}' " +
                                $"FOR UPDATE")
                    .SingleAsync(cancellationToken);

                if (entry.State == OutboxEntryState.Pending)
                {
                    var headers = JsonSerializer.Deserialize<Dictionary<string, string>>(entry.Headers);
                    var messageType = Type.GetType(entry.TypeName);
                    var message = JsonSerializer.Deserialize(entry.Body, messageType!);

                    try
                    {
                        await publishEndpoint.Publish(message, context =>
                        {
                            foreach (var (key, value) in headers)
                            {
                                context.Headers.Set(key, value);
                            }
                        }, cancellationToken);
                        entry.State = OutboxEntryState.Sent;
                    }
                    catch (Exception e)
                    {
                        logger.LogError(e,
                            $"Failed to dispatch a message due to a network error. Message id: {entry.Id}");

                        entry.Retries++;
                        if (entry.Retries > options.Value.RetryCount)
                        {
                            entry.State = OutboxEntryState.Failed;
                        }
                    }

                    await context.SaveChangesAsync(CancellationToken.None);
                }

                await transaction.CommitAsync(CancellationToken.None);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Failed to dispatch a message due to a local error.");
                await transaction.RollbackAsync(CancellationToken.None);
            }
        }
    }
}