using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.IdentityProvider.Application.Features.TenantUsers.Common;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Domain.Entities.Users;
using ESchool.Libs.Application.IntegrationEvents.UserCreation;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.IdentityProvider.Application.Features.TenantUsers
{
    public class TenantUserCreateCommand : IRequest<TenantUserDetailsResponse>
    {
        public string Email { get; set; }
        public IEnumerable<TenantRoleType> Roles { get; set; }
    }

    public class TenantUserCreateHandler : IRequestHandler<TenantUserCreateCommand, TenantUserDetailsResponse>
    {
        private readonly IdentityProviderContext context;
        private readonly IIdentityService identityService;
        private readonly IPublishEndpoint publishEndpoint;
        private readonly IMapper mapper;

        public TenantUserCreateHandler(IdentityProviderContext context, IIdentityService identityService, IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            this.context = context;
            this.identityService = identityService;
            this.publishEndpoint = publishEndpoint;
            this.mapper = mapper;
        }
        
        public async Task<TenantUserDetailsResponse> Handle(TenantUserCreateCommand request, CancellationToken cancellationToken)
        {
            var tenantId = identityService.GetTenantId();
            var user = await context.Users.Include(x => x.TenantUsers)
                    .ThenInclude(x => x.TenantUserRoles)
                .SingleOrDefaultAsync(x => x.NormalizedEmail == request.Email.ToUpper(), cancellationToken);
            if (user == null)
            {
                user = new User
                {
                    Email = request.Email,
                    GlobalRole = GlobalRoleType.TenantUser,
                    TenantUsers = new List<TenantUser>
                    {
                        new TenantUser
                        {
                            TenantId = tenantId,
                            TenantUserRoles = request.Roles.Select(x => new TenantUserRole
                            {
                                TenantRole = x
                            }).ToList()
                        }
                    },
                    DefaultTenantId = tenantId
                };
                context.Users.Add(user);
            }
            else
            {
                if (user.TenantUsers.Any(x => x.TenantId == tenantId))
                {
                    throw new InvalidOperationException("The user is already the member of this tenant.");
                }
                var tenantUser = new TenantUser
                {
                    UserId = user.Id,
                    TenantId = tenantId,
                    TenantUserRoles = request.Roles.Select(x => new TenantUserRole
                    {
                        TenantRole = x
                    }).ToList()
                };
                context.TenantUsers.Add(tenantUser);
            }

            await context.SaveChangesAsync(cancellationToken);
            await publishEndpoint.Publish(mapper.Map<UserCreatedIntegrationEvent>(user), cancellationToken);
            return new TenantUserDetailsResponse
            {
                Id = user.Id,
                Email = user.Email,
                TenantRoleTypes = user.TenantUsers.Single(x => x.TenantId == tenantId).TenantUserRoles
                    .Select(x => x.TenantRole)
            };
        }
    }
}