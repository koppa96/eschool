using System;
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
        private readonly OutboxDbContext outboxDbContext;

        public EfCoreEventPublisher(IPublishFilterRunner publishFilterRunner, OutboxDbContext outboxDbContext)
        {
            this.publishFilterRunner = publishFilterRunner;
            this.outboxDbContext = outboxDbContext;
        }
        
        public async Task PublishAsync<TMessage>(TMessage message, Func<OutboxPublishContext<TMessage>, Task> inlineFilter = null)
        {
            var context = await publishFilterRunner.RunFiltersAsync(message);
            if (inlineFilter != null)
            {
                await inlineFilter(context);
            }
            var entity = OutboxEntry.FromPublishContext(context);

            outboxDbContext.OutboxEntries.Add(entity);
        }
    }
}