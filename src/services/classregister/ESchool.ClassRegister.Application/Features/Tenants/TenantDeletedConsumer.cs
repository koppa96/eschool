using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.IdentityProvider.Interface.IntegrationEvents.Tenants;
using ESchool.Libs.Domain.Extensions;
using ESchool.Libs.Domain.MultiTenancy;
using ESchool.Libs.Outbox.EntityFrameworkCore;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Tenants
{
    public class TenantDeletedConsumer : IConsumer<TenantDeletedEvent>
    {
        private readonly DbContextOptions<ClassRegisterContext> dbContextOptions;
        private readonly MasterDbContext masterDbContext;
        private readonly OutboxDbContext outboxDbContext;

        public TenantDeletedConsumer(DbContextOptions<ClassRegisterContext> dbContextOptions,
            MasterDbContext masterDbContext,
            OutboxDbContext outboxDbContext)
        {
            this.dbContextOptions = dbContextOptions;
            this.masterDbContext = masterDbContext;
            this.outboxDbContext = outboxDbContext;
        }
        
        public async Task Consume(ConsumeContext<TenantDeletedEvent> context)
        {
            var tenant = await masterDbContext.Tenants.FindOrThrowAsync(context.Message.TenantId);
            await using var tenantDbContext = new ClassRegisterContext(dbContextOptions, tenant);
            
            await tenantDbContext.Database.EnsureDeletedAsync();
            masterDbContext.Tenants.Remove(tenant);
            
            await masterDbContext.SaveChangesAsync();
        }
    }
}