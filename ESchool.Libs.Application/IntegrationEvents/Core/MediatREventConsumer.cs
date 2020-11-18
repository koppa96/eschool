using System;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ESchool.Libs.Application.IntegrationEvents.Core
{
    public class MediatREventConsumer<TEvent> : IConsumer<TEvent>
        where TEvent : class, IRequest
    {
        private readonly IMediator mediator;
        private readonly ILogger<MediatREventConsumer<TEvent>> logger;

        public MediatREventConsumer(IMediator mediator, ILogger<MediatREventConsumer<TEvent>> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }
        
        public async Task Consume(ConsumeContext<TEvent> context)
        {
            try
            {
                await mediator.Send(context.Message);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                throw;
            }
        }
    }
}