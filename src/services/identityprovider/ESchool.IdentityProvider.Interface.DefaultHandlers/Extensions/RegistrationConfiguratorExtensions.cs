using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ESchool.IdentityProvider.Interface.DefaultHandlers.Extensions
{
    public static class RegistrationConfiguratorExtensions
    {
        public static IRegistrationConfigurator AddTenantEventConsumers<TContext>(this IRegistrationConfigurator configurator)
            where TContext : DbContext
        {
            configurator.AddConsumer<TenantCreatedOrUpdatedConsumer<TContext>>();
            configurator.AddConsumer<TenantDeletedConsumer<TContext>>();
            return configurator;
        }
    }
}