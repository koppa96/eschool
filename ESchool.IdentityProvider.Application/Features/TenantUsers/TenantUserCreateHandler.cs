using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Application.Features.TenantUsers.Common;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Domain.Entities.Users;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

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
        private readonly UserManager<User> userManager;
        private readonly IIdentityService identityService;

        public TenantUserCreateHandler(IdentityProviderContext context, UserManager<User> userManager, IIdentityService identityService)
        {
            this.context = context;
            this.userManager = userManager;
            this.identityService = identityService;
        }
        
        public async Task<TenantUserDetailsResponse> Handle(TenantUserCreateCommand request, CancellationToken cancellationToken)
        {
            var tenantId = identityService.GetTenantId();
            var user = await userManager.FindByEmailAsync(request.Email);
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