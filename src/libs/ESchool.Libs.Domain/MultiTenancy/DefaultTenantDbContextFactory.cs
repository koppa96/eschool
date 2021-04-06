using System;
using System.Linq;
using ESchool.Libs.Domain.MultiTenancy.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ESchool.Libs.Domain.MultiTenancy
{
    public class DefaultTenantDbContextFactory<TContext> : ITenantDbContextFactory<TContext>
        where TContext : DbContext
    {
        private readonly IServiceProvider serviceProvider;

        public DefaultTenantDbContextFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        
        public TContext CreateContext(Tenant tenant)
        {
            var ctor = typeof(TContext).GetConstructors()
                .Single();
            
            var arguments = ctor.GetParameters()
                .Select(x => x.ParameterType == typeof(Tenant)
                    ? tenant
                    : serviceProvider.GetRequiredService(x.ParameterType))
                .ToList();

            return (TContext) ctor.Invoke(arguments.ToArray());
        }
    }
}