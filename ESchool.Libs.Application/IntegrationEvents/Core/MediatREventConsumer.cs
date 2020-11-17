using System.Threading.Tasks;
using MassTransit;
using MediatR;

namespace ESchool.Libs.Application.IntegrationEvents.Core
{
    public class MediatREventConsumer<TEvent> : IConsumer<TEvent>
        where TEvent : class, IRequest
    {
        private readonly IMediator mediator;

        public MediatREventConsumer(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        public Task Consume(ConsumeContext<TEvent> context)
        {
            return mediator.Send(context.Message);
        }
    }
}