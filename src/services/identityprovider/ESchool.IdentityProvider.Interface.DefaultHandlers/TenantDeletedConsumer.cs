using System.Threading.Tasks;
using ESchool.IdentityProvider.Interface.IntegrationEvents.Tenants;
using ESchool.Libs.Domain.Extensions;
using ESchool.Libs.Domain.MultiTenancy;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ESchool.IdentityProvider.Interface.DefaultHandlers
{
    public class TenantDeletedConsumer<TContext> : IConsumer<TenantDeletedEvent>
        where TContext : DbContext
    {
        private readonly MasterDbContext masterDbContext;
        private readonly ITenantDbContextFactory<TContext> tenantDbContextFactory;

        public TenantDeletedConsumer(
            MasterDbContext masterDbContext,
            ITenantDbContextFactory<TContext> tenantDbContextFactory)
        {
            this.masterDbContext = masterDbContext;
            this.tenantDbContextFactory = tenantDbContextFactory;
        }

        public async Task Consume(ConsumeContext<TenantDeletedEvent> context)
        {
            var tenant = await masterDbContext.Tenants.FindOrThrowAsync(context.Message.TenantId);
            await using var tenantDbContext = tenantDbContextFactory.CreateContext(tenant);

            await tenantDbContext.Database.EnsureDeletedAsync();
            masterDbContext.Tenants.Remove(tenant);

            await masterDbContext.SaveChangesAsync();
        }
    }
}