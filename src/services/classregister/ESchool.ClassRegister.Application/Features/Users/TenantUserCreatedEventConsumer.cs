using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Attributes;
using ESchool.ClassRegister.Domain.Entities.Users;
using ESchool.IdentityProvider.Interface.IntegrationEvents.TenantUsers;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Users
{
    public class TenantUserCreatedEventConsumer : IConsumer<TenantUserCreatedIntegrationEvent>
    {
        private readonly ClassRegisterContext dbContext;

        public TenantUserCreatedEventConsumer(ClassRegisterContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<TenantUserCreatedIntegrationEvent> context)
        {
            var tenantUserTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.BaseType == typeof(UserBase) && x.GetCustomAttribute<TenantUserAttribute>() != null)
                .ToList();
            
            var existingUserBases = await dbContext.UserBases.IgnoreQueryFilters()
                .Where(x => x.UserId == context.Message.UserId)
                .ToListAsync();

            foreach (var roleType in context.Message.TenantRoleTypes)
            {
                var existingUserBase = existingUserBases.SingleOrDefault(x =>
                    x.GetType().GetCustomAttribute<TenantUserAttribute>()?.TenantRoleType == roleType);

                if (existingUserBase == null)
                {
                    var userType = tenantUserTypes.Single(x =>
                        x.GetCustomAttribute<TenantUserAttribute>()!.TenantRoleType == roleType);

                    var user = (UserBase)Activator.CreateInstance(userType);
                    user.Email = context.Message.Email;
                    dbContext.UserBases.Add(user);
                }
                else
                {
                    existingUserBase.IsDeleted = false;
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}