using System;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace ESchool.Libs.AspNetCore.Filters
{
    public class MessageLoggerConsumeFilter<T> : IFilter<ConsumeContext<T>>
        where T : class
    {
        private readonly ILogger<MessageLoggerConsumeFilter<T>> logger;

        public MessageLoggerConsumeFilter(ILogger<MessageLoggerConsumeFilter<T>> logger)
        {
            this.logger = logger;
        }
        
        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            try
            {
                await next.Send(context);
                logger.LogDebug($"Successfully processed a message of type {context.Message.GetType().FullName}.");
            }
            catch(Exception e)
            {
                logger.LogError(e, $"An error occured when trying to process a message of type {context.Message.GetType().FullName}.");
                throw;
            }
        }

        public void Probe(ProbeContext context)
        {
        }
    }
}