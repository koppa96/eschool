using System;
using ESchool.Libs.Outbox.AspNetCore.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ESchool.Libs.Outbox.AspNetCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMassTransitOutbox<TPrimaryKey>(
            this IServiceCollection services,
            Action<IOutboxConfigurator> configureAction
        )
            where TPrimaryKey : IEquatable<TPrimaryKey>
        {
            var outboxConfigurator = new OutboxConfigurator(services);
            configureAction(outboxConfigurator);

            services.AddHostedService<MessageDispatcherHostedService<TPrimaryKey>>();
            
            return services;
        }
    }
}