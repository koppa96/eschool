using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Attributes;
using ESchool.ClassRegister.Domain.Entities.Users;
using ESchool.Libs.Application.IntegrationEvents.TenantUsers;
using ESchool.Libs.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Users
{
    public class TenantUserCreatedHandler : IRequestHandler<TenantUserCreatedIntegrationEvent>
    {
        private readonly ClassRegisterContext context;

        public TenantUserCreatedHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public async Task<Unit> Handle(TenantUserCreatedIntegrationEvent request, CancellationToken cancellationToken)
        {
            var tenantUserTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.BaseType == typeof(UserBase) && x.GetCustomAttribute<TenantUserAttribute>() != null)
                .ToList();
            
            var existingUserBases = await context.UserBases.IgnoreQueryFilters()
                .Where(x => x.UserId == request.UserId && x.TenantId == request.TenantId)
                .ToListAsync(cancellationToken);

            foreach (var roleType in request.TenantRoleTypes)
            {
                var existingUserBase = existingUserBases.SingleOrDefault(x =>
                    x.GetType().GetCustomAttribute<TenantUserAttribute>()?.TenantRoleType == roleType);

                if (existingUserBase == null)
                {
                    var userType = tenantUserTypes.Single(x =>
                        x.GetCustomAttribute<TenantUserAttribute>()!.TenantRoleType == roleType);

                    var user = (UserBase)Activator.CreateInstance(userType);
                    user.Email = request.Email;
                    user.TenantId = request.TenantId;
                }
                else
                {
                    existingUserBase.IsDeleted = false;
                }
            }
        }
    }
}