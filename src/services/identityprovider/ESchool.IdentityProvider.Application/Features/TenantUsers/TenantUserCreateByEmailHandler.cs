using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Application.Features.TenantUsers.Common;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Domain.Entities.Users;
using ESchool.IdentityProvider.Interface.IntegrationEvents.TenantUsers;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;
using ESchool.Libs.Outbox.Services;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.IdentityProvider.Application.Features.TenantUsers
{
    public class TenantUserCreateByEmailCommand : IRequest<TenantUserDetailsResponse>
    {
        public string Email { get; set; }
        public IEnumerable<TenantRoleType> Roles { get; set; }
    }

    public class
        TenantUserCreateByEmailHandler : IRequestHandler<TenantUserCreateByEmailCommand, TenantUserDetailsResponse>
    {
        private readonly IdentityProviderContext context;
        private readonly IIdentityService identityService;
        private readonly IEventPublisher publisher;

        public TenantUserCreateByEmailHandler(IdentityProviderContext context, IIdentityService identityService,
            IEventPublisher publisher)
        {
            this.context = context;
            this.identityService = identityService;
            this.publisher = publisher;
        }

        public async Task<TenantUserDetailsResponse> Handle(TenantUserCreateByEmailCommand request,
            CancellationToken cancellationToken)
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
                    throw new InvalidOperationException("The user is already the member of this tenant.");
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

            await publisher.PublishAsync(new TenantUserCreatedOrEditedEvent
            {
                UserId = user.Id,
                TenantId = tenantId
            }, CancellationToken.None);
            await context.SaveChangesAsync(cancellationToken);

            return new TenantUserDetailsResponse
            {
                Id = user.Id,
                Email = user.Email,
                TenantRoles = user.TenantUsers.Single(x => x.TenantId == tenantId).TenantUserRoles
                    .Select(x => x.TenantRole)
            };
        }
    }
}