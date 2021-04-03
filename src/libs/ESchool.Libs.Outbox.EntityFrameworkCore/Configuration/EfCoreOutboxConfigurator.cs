using ESchool.Libs.Outbox.EntityFrameworkCore.Services;
using ESchool.Libs.Outbox.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ESchool.Libs.Outbox.EntityFrameworkCore.Configuration
{
    public class EfCoreOutboxConfigurator
    {
        public IServiceCollection Services { get; }

        public EfCoreOutboxConfigurator(IServiceCollection services)
        {
            Services = services;
        }

        public EfCoreOutboxConfigurator UseStandardMessageDispatcher()
        {
            Services.AddTransient<IMessageDispatcher, EfCoreMessageDispatcher>();
            return this;
        }

        public EfCoreOutboxConfigurator UseMultiTenantMessageDispatcher()
        {
            Services.AddTransient<IMessageDispatcher, EfCoreMultiTenantMessageDispatcher>();
            return this;
        }

        public EfCoreOutboxConfigurator UseTenantOutboxDbContextFactory<TFactory>()
            where TFactory : class, ITenantOutboxDbContextFactory
        {
            Services.AddTransient<ITenantOutboxDbContextFactory, TFactory>();
            return this;
        }
    }
}