using System.Threading.Tasks;
using ESchool.Libs.Application.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace ESchool.ClassRegister.Api.Consumers
{
    public class UserCreatedEventConsumer : IConsumer<UserCreatedIntegrationEvent>
    {
        private readonly ILogger<UserCreatedEventConsumer> logger;

        public UserCreatedEventConsumer(ILogger<UserCreatedEventConsumer> logger)
        {
            this.logger = logger;
        }
        
        public Task Consume(ConsumeContext<UserCreatedIntegrationEvent> context)
        {
            logger.LogInformation("User creation event received.");
            return Task.CompletedTask;
        }
    }
}