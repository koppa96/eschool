using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.IdentityProvider.Interface.IntegrationEvents.Tenants;
using ESchool.Libs.Domain.Extensions;
using ESchool.Libs.Domain.MultiTenancy;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Tenants
{
    public class TenantDeletedConsumer : IConsumer<TenantDeletedEvent>
    {
        private readonly MasterDbContext masterDbContext;
        private readonly ITenantDbContextFactory<ClassRegisterContext> tenantDbContextFactory;

        public TenantDeletedConsumer(
            MasterDbContext masterDbContext,
            ITenantDbContextFactory<ClassRegisterContext> tenantDbContextFactory)
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