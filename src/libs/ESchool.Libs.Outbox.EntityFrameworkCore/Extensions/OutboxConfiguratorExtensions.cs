using System;
using ESchool.Libs.Outbox.EntityFrameworkCore.Configuration;
using ESchool.Libs.Outbox.EntityFrameworkCore.Services;
using ESchool.Libs.Outbox.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ESchool.Libs.Outbox.EntityFrameworkCore.Extensions
{
    public static class OutboxConfiguratorExtensions
    {
        public static IOutboxConfigurator UseEntityFrameworkCore<TContext>(
            this IOutboxConfigurator configurator,
            Action<EfCoreOutboxConfigurator<TContext>> configureAction
        )
            where TContext : DbContext, IOutboxDbContext
        {
            configurator.Services.AddScoped<IOutboxDbContext>(provider => provider.GetRequiredService<TContext>());
            configurator.Services.AddScoped<IEventPublisher, EfCoreEventPublisher>();
            
            configureAction?.Invoke(new EfCoreOutboxConfigurator<TContext>(configurator.Services));
            
            return configurator;
        }
    }
}