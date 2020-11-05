using System.Threading.Tasks;
using ESchool.Libs.Application.IntegrationEvents;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ESchool.ClassRegister.Api.Consumers
{
    public class UserCreatedEventConsumer : IConsumer<UserCreatedIntegrationEvent>
    {
        private readonly IMediator mediator;

        public UserCreatedEventConsumer(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        public Task Consume(ConsumeContext<UserCreatedIntegrationEvent> context)
        {
            return mediator.Send(context.Message);
        }
    }
}