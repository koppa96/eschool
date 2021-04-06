using ESchool.Libs.Outbox.EntityFrameworkCore.Services;
using ESchool.Libs.Outbox.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ESchool.Libs.Outbox.EntityFrameworkCore.Configuration
{
    public class EfCoreOutboxConfigurator<TContext>
        where TContext : DbContext, IOutboxDbContext
    {
        public IServiceCollection Services { get; }

        public EfCoreOutboxConfigurator(IServiceCollection services)
        {
            Services = services;
        }

        public EfCoreOutboxConfigurator<TContext> UseStandardMessageDispatcher()
        {
            Services.AddTransient<IMessageDispatcher, EfCoreMessageDispatcher>();
            return this;
        }

        public EfCoreOutboxConfigurator<TContext> UseMultiTenantMessageDispatcher()
        {
            Services.AddTransient<IMessageDispatcher, EfCoreMultiTenantMessageDispatcher<TContext>>();
            return this;
        }
    }
}