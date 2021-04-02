using Microsoft.Extensions.DependencyInjection;

namespace ESchool.Libs.Outbox.AspNetCore.Configuration
{
    public class OutboxConfigurator : IOutboxConfigurator
    {
        public OutboxConfigurator(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}