using System;
using ESchool.Libs.Outbox.EntityFrameworkCore.Services;
using ESchool.Libs.Outbox.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ESchool.Libs.Outbox.EntityFrameworkCore.Extensions
{
    public static class OutboxConfiguratorExtensions 
    {
        public static IOutboxConfigurator UseEntityFrameworkCore(this IOutboxConfigurator configurator, Action<DbContextOptionsBuilder> configureDbContext)
        {
            configurator.Services.AddDbContext<OutboxDbContext>(configureDbContext);
            configurator.Services.AddScoped<IMessageDispatcher, EfCoreMessageDispatcher>();
            configurator.Services.AddScoped<IEventPublisher, EfCoreEventPublisher>();
            return configurator;
        }
    }
}