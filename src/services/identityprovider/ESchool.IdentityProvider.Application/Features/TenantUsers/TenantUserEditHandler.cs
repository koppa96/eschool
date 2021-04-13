using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.IdentityProvider.Application.Features.TenantUsers.Common;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Domain.Entities.Users;
using ESchool.IdentityProvider.Interface.IntegrationEvents.TenantUsers;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;
using ESchool.Libs.Outbox.Services;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.IdentityProvider.Application.Features.TenantUsers
{
    public class TenantUserEditCommand
    {
        public IEnumerable<TenantRoleType> TenantRoleTypes { get; set; }
    }

    public class TenantUserEditHandler : IRequestHandler<EditCommand<TenantUserEditCommand, TenantUserDetailsResponse>,
        TenantUserDetailsResponse>
    {
        private readonly IdentityProviderContext context;
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;
        private readonly IEventPublisher publisher;

        public TenantUserEditHandler(IdentityProviderContext context, IIdentityService identityService,
            IEventPublisher publisher, IMapper mapper)
        {
            this.context = context;
            this.identityService = identityService;
            this.publisher = publisher;
            this.mapper = mapper;
        }

        public async Task<TenantUserDetailsResponse> Handle(
            EditCommand<TenantUserEditCommand, TenantUserDetailsResponse> request,
            CancellationToken cancellationToken)
        {
            var tenantId = identityService.GetTenantId();
            var tenantUser = await context.TenantUsers.Include(x => x.TenantUserRoles)
                .Include(x => x.User)
                    .ThenInclude(x => x.TenantUsers)
                    .ThenInclude(x => x.TenantUserRoles)
                .SingleAsync(x => x.UserId == request.Id && x.TenantId == tenantId, cancellationToken);

            context.TenantUserRoles.RemoveRange(tenantUser.TenantUserRoles);
            context.TenantUserRoles.AddRange(request.InnerCommand.TenantRoleTypes.Select(x => new TenantUserRole
            {
                TenantUserId = tenantUser.Id,
                TenantRole = x
            }));
            
            await publisher.PublishAsync(new TenantUserCreatedOrEditedEvent
            {
                UserId = tenantUser.UserId,
                TenantId = tenantId,
                Email = tenantUser.User.Email,
                TenantRoles = tenantUser.TenantUserRoles.Select(x => x.TenantRole)
                    .ToList()
            }, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return new TenantUserDetailsResponse
            {
                Id = tenantUser.UserId,
                Email = tenantUser.User.Email,
                TenantRoles = tenantUser.TenantUserRoles.Select(x => x.TenantRole)
            };
        }
    }
}