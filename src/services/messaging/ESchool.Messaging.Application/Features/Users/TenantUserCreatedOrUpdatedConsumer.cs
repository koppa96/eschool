using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Interface.IntegrationEvents.TenantUsers;
using ESchool.Libs.Domain.MultiTenancy;
using ESchool.Messaging.Domain;
using ESchool.Messaging.Domain.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Messaging.Application.Features.Users
{
    public class TenantUserCreatedOrUpdatedConsumer : IConsumer<TenantUserCreatedOrEditedEvent>
    {
        private readonly MasterDbContext masterDbContext;
        private readonly ITenantDbContextFactory<MessagingContext> tenantDbContextFactory;

        public TenantUserCreatedOrUpdatedConsumer(
            MasterDbContext masterDbContext,
            ITenantDbContextFactory<MessagingContext> tenantDbContextFactory)
        {

            this.masterDbContext = masterDbContext;
            this.tenantDbContextFactory = tenantDbContextFactory;
        }

        public async Task Consume(ConsumeContext<TenantUserCreatedOrEditedEvent> context)
        {
            var tenant = await masterDbContext.Tenants.FindAsync(context.Message.TenantId);
            
            // Global admins can also create users => No tenant Id will be set in the Identity Service.
            await using var dbContext = tenantDbContextFactory.CreateContext(tenant);

            var existingUser = await dbContext.Users.IgnoreQueryFilters()
                .Include(x => x.UserRoles)
                .SingleOrDefaultAsync(x => x.Id == context.Message.UserId);

            if (existingUser == null)
            {
                existingUser = new MessagingUser
                {
                    Id = context.Message.UserId,
                    Name = context.Message.Name,
                    UserRoles = new List<MessagingUserRole>()
                };
            }
            else
            {
                dbContext.UserRoles.RemoveRange(existingUser.UserRoles);
                
                if (existingUser.IsDeleted)
                {
                    existingUser.IsDeleted = false;
                }
            }

            var userRoles = context.Message.TenantRoles.Select(x => new MessagingUserRole
            {
                User = existingUser,
                TenantRoleType = x,
            });
            
            dbContext.UserRoles.AddRange(userRoles);
            await dbContext.SaveChangesAsync();
        }
    }
}