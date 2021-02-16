using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Attributes;
using ESchool.ClassRegister.Domain.Entities.Users;
using ESchool.Libs.Application.IntegrationEvents.TenantUsers;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Users
{
    public class TenantUserEditedEventConsumer : IConsumer<TenantUserEditedIntegrationEvent>
    {
        private readonly ClassRegisterContext dbContext;

        public TenantUserEditedEventConsumer(ClassRegisterContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public async Task Consume(ConsumeContext<TenantUserEditedIntegrationEvent> context)
        {
            var tenantUserTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.BaseType == typeof(UserBase) && x.GetCustomAttribute<TenantUserAttribute>() != null)
                .ToList();
            
            var userBases = await dbContext.UserBases.IgnoreQueryFilters()
                .Where(x => x.UserId == context.Message.UserId && x.TenantId == context.Message.TenantId)
                .ToListAsync();
            
            // Delete or update userBases
            foreach (var userBase in userBases)
            {
                userBase.Email = context.Message.Email;
                if (context.Message.TenantRoleTypes.All(x => x != userBase.GetType().GetCustomAttribute<TenantUserAttribute>()!.TenantRoleType))
                {
                    dbContext.UserBases.Remove(userBase);
                }
            }

            foreach (var roleType in context.Message.TenantRoleTypes.Where(x => userBases
                .All(ub => ub.GetType().GetCustomAttribute<TenantUserAttribute>()!.TenantRoleType != x)))
            {
                var userType = tenantUserTypes.Single(x =>
                    x.GetCustomAttribute<TenantUserAttribute>()!.TenantRoleType == roleType);

                var user = (UserBase)Activator.CreateInstance(userType);
                user.Email = context.Message.Email;
                user.TenantId = context.Message.TenantId;
                dbContext.UserBases.Add(user);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}