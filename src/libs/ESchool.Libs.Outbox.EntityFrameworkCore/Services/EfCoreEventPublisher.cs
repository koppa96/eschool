using System;
using System.Linq;
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
        private IOutboxDbContext outboxDbContext;

        public EfCoreEventPublisher(IPublishFilterRunner publishFilterRunner)
        {
            this.publishFilterRunner = publishFilterRunner;
        }

        public void Setup(params object[] @params)
        {
            var context = @params.OfType<IOutboxDbContext>()
                .LastOrDefault();

            outboxDbContext = context;
        }

        public Task PublishAsync(object message, CancellationToken cancellationToken = default)
        {
            return PublishAsync(message, _ => Task.CompletedTask, cancellationToken);
        }

        public async Task PublishAsync(object message, Func<OutboxPublishContext, Task> inlineFilter, CancellationToken cancellationToken = default)
        {
            var context = await publishFilterRunner.RunFiltersAsync(message, cancellationToken);
            if (!context.Canceled)
            {
                await inlineFilter(context);

                var entity = OutboxEntry.FromPublishContext(context);
                outboxDbContext.OutboxEntries.Add(entity);
            }
        }
    }
}