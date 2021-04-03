using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Outbox.EntityFrameworkCore.Entities;
using ESchool.Libs.Outbox.Filters;
using ESchool.Libs.Outbox.Models;
using ESchool.Libs.Outbox.Services;

namespace ESchool.Libs.Outbox.EntityFrameworkCore.Services
{
    public class EfCoreEventPublisher : IEventPublisher
    {
        private readonly IPublishFilterRunner publishFilterRunner;
        private readonly IOutboxDbContext outboxDbContext;

        public EfCoreEventPublisher(IPublishFilterRunner publishFilterRunner, IOutboxDbContext outboxDbContext)
        {
            this.publishFilterRunner = publishFilterRunner;
            this.outboxDbContext = outboxDbContext;
        }

        public Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default)
        {
            return PublishAsync(message, _ => Task.CompletedTask, cancellationToken);
        }

        public async Task PublishAsync<TMessage>(TMessage message, Func<OutboxPublishContext<TMessage>, Task> inlineFilter, CancellationToken cancellationToken = default)
        {
            var context = await publishFilterRunner.RunFiltersAsync(message, cancellationToken);
            await inlineFilter(context);
            
            var entity = OutboxEntry.FromPublishContext(context);
            outboxDbContext.OutboxEntries.Add(entity);
        }
    }
}