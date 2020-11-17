using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Attributes;
using ESchool.ClassRegister.Domain.Entities.Users;
using ESchool.Libs.Application.IntegrationEvents;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.Users
{
    public class UserCreateHandler : IRequestHandler<UserCreatedIntegrationEvent, Unit>
    {
        private readonly ClassRegisterContext context;

        public UserCreateHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public async Task<Unit> Handle(UserCreatedIntegrationEvent request, CancellationToken cancellationToken)
        {
            var tenantUserTypes = typeof(UserBase).Assembly
                .GetTypes()
                .Where(x => x.GetCustomAttribute<TenantUserAttribute>() != null)
                .ToList();
            
            foreach (var tenantRole in request.TenantRoles)
            {
                foreach (var role in tenantRole.Roles)
                {
                    var userTypeForRole = tenantUserTypes.Single(x =>
                        x.GetCustomAttribute<TenantUserAttribute>().TenantRoleType == role.TenantRoleType);
                    
                    var instance = (UserBase)Activator.CreateInstance(userTypeForRole);
                    instance.Id = role.Id;
                    instance.Email = request.Email;
                    instance.TenantId = tenantRole.TenantId;
                    context.UserBases.Add(instance);
                }
            }

            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}