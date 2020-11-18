using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Attributes;
using ESchool.ClassRegister.Domain.Entities.Users;
using ESchool.Libs.Application.IntegrationEvents;
using ESchool.Libs.Application.IntegrationEvents.UserCreation;
using ESchool.Libs.Domain.Enums;
using MassTransit;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.Users
{
    public class UserCreateHandler : IRequestHandler<UserCreatedIntegrationEvent, Unit>
    {
        private readonly ClassRegisterContext context;
        private readonly IPublishEndpoint publishEndpoint;

        public UserCreateHandler(ClassRegisterContext context, IPublishEndpoint publishEndpoint)
        {
            this.context = context;
            this.publishEndpoint = publishEndpoint;
        }
        
        public async Task<Unit> Handle(UserCreatedIntegrationEvent request, CancellationToken cancellationToken)
        {
            var events = new List<object>();
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
                    instance.Name = request.UserName;
                    instance.Email = request.Email;
                    instance.TenantId = tenantRole.TenantId;
                    context.UserBases.Add(instance);

                    if (role.TenantRoleType == TenantRoleType.Student)
                    {
                        events.Add(new StudentCreatedIntegrationEvent
                        {
                            Id = instance.Id,
                            Name = request.UserName
                        });
                    }
                    else
                    {
                        events.Add(new TeacherCreatedIntegrationEvent
                        {
                            Id = instance.Id,
                            Name = request.UserName
                        });
                    }
                }
            }

            await context.SaveChangesAsync(cancellationToken);
            foreach (var @event in events)
            {
                await publishEndpoint.Publish(@event);
            }
            return Unit.Value;
        }
    }
}