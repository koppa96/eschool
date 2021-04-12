using System;
using System.Linq;
using System.Reflection;
using ESchool.Libs.Domain.MultiTenancy.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ESchool.Libs.Domain.MultiTenancy
{
    public class DefaultTenantDbContextFactory<TContext> : ITenantDbContextFactory<TContext>
        where TContext : DbContext
    {
        private static readonly ConstructorInfo Constructor = typeof(TContext)
            .GetConstructors()
            .First();
        
        private static readonly Type[] ParameterTypes = Constructor.GetParameters()
            .Select(x => x.ParameterType)
            .ToArray();
        
        private readonly IServiceProvider serviceProvider;

        public DefaultTenantDbContextFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        
        public TContext CreateContext(Tenant tenant)
        {
            var arguments = ParameterTypes
                .Select(x => x == typeof(Tenant)
                    ? tenant
                    : serviceProvider.GetRequiredService(x))
                .ToArray();

            return (TContext) Constructor.Invoke(arguments);
        }
    }
}