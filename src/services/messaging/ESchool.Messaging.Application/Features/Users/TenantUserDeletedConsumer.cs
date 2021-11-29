using System.Threading.Tasks;
using ESchool.IdentityProvider.Interface.IntegrationEvents.TenantUsers;
using ESchool.Libs.Domain.MultiTenancy;
using ESchool.Messaging.Domain;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Messaging.Application.Features.Users
{
    public class TenantUserDeletedConsumer : IConsumer<TenantUserDeletedEvent>
    {
        private readonly ITenantDbContextFactory<MessagingContext> tenantDbContextFactory;
        private readonly MasterDbContext masterDbContext;

        public TenantUserDeletedConsumer(
            ITenantDbContextFactory<MessagingContext> tenantDbContextFactory,
            MasterDbContext masterDbContext)
        {
            this.tenantDbContextFactory = tenantDbContextFactory;
            this.masterDbContext = masterDbContext;
        }

        public async Task Consume(ConsumeContext<TenantUserDeletedEvent> context)
        {
            var tenant = await masterDbContext.Tenants.FindAsync(context.Message.TenantId);
            await using var dbContext = tenantDbContextFactory.CreateContext(tenant);

            var userWithRoles = await dbContext.Users.Include(x => x.UserRoles)
                .SingleAsync(x => x.Id == context.Message.UserId);

            dbContext.Users.Remove(userWithRoles);
            await dbContext.SaveChangesAsync();
        }
    }
}