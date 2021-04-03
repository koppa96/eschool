using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Outbox.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ESchool.Libs.Outbox.Filters
{
    public class DefaultPublishFilterRunner : IPublishFilterRunner
    {
        private readonly IServiceProvider serviceProvider;

        public DefaultPublishFilterRunner(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        
        public async Task<OutboxPublishContext<TMessage>> RunFiltersAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default)
        {
            var publishFilters = serviceProvider.GetServices<IPublishFilter<TMessage>>();
            
            var context = new OutboxPublishContext<TMessage>(message);
            foreach (var filter in publishFilters)
            {
                if (context.Canceled)
                {
                    break;
                }
                
                await filter.ExecuteAsync(context, cancellationToken);
            }

            return context;
        }
    }
}