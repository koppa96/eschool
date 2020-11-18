using AutoMapper;
using ESchool.IdentityProvider.Application.Features.Users.Common;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Domain.Entities.Users;
using ESchool.Libs.Domain;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Application.IntegrationEvents;
using ESchool.Libs.Application.IntegrationEvents.UserCreation;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;

namespace ESchool.IdentityProvider.Application.Features.Users
{
    public class UserCreateCommand : IRequest<UserDetailsResponse>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public GlobalRoleType GlobalRole { get; set; }
        public IEnumerable<TenantUserCreateModel> Tenants { get; set; }
        
        public class TenantUserCreateModel
        {
            public Guid TenantId { get; set; }
            public IEnumerable<TenantRoleType> Roles { get; set; }
        }
    }

    public class UserCreateHandler : IRequestHandler<UserCreateCommand, UserDetailsResponse>
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly IPublishEndpoint publishEndpoint;

        public UserCreateHandler(
            UserManager<User> userManager,
            IMapper mapper,
            IPublishEndpoint publishEndpoint)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task<UserDetailsResponse> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                GlobalRole = request.GlobalRole,
                TenantUsers = request.Tenants.Select(x => new TenantUser
                {
                    TenantId = x.TenantId,
                    TenantUserRoles = x.Roles.Select(r => new TenantUserRole
                    {
                        TenantRole = r
                    }).ToList()
                }).ToList()
            };

            var result = await userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                await publishEndpoint.Publish(mapper.Map<UserCreatedIntegrationEvent>(user), cancellationToken);
                return mapper.Map<UserDetailsResponse>(user);
            }

            throw new InvalidOperationException("Could not create user. Username or email is already taken.");
        }
    }
}
