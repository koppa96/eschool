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
using ESchool.Libs.Outbox.Services;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.IdentityProvider.Application.Features.TenantUsers
{
    public class TenantUserCreateByIdCommand : IRequest<TenantUserDetailsResponse>
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public List<TenantRoleType> TenantRoleTypes { get; set; }
    }

    public class TenantUserCreateByIdHandler : IRequestHandler<TenantUserCreateByIdCommand, TenantUserDetailsResponse>
    {
        private readonly IdentityProviderContext context;
        private readonly IEventPublisher publisher;

        public TenantUserCreateByIdHandler(IdentityProviderContext context, IEventPublisher publisher)
        {
            this.context = context;
            this.publisher = publisher;
        }

        public async Task<TenantUserDetailsResponse> Handle(TenantUserCreateByIdCommand request,
            CancellationToken cancellationToken)
        {
            var user = await context.Users.Include(x => x.TenantUsers)
                .ThenInclude(x => x.TenantUserRoles)
                .SingleAsync(x => x.Id == request.UserId, cancellationToken);

            if (user.TenantUsers.Any(x => x.TenantId == request.TenantId))
                throw new InvalidOperationException("The user is already the member of this tenant.");

            context.TenantUsers.Add(new TenantUser
            {
                TenantId = request.TenantId,
                UserId = user.Id,
                TenantUserRoles = request.TenantRoleTypes.Select(x => new TenantUserRole
                {
                    TenantRole = x
                }).ToList()
            });

            await publisher.PublishAsync(new TenantUserCreatedOrEditedEvent
            {
                UserId = user.Id,
                Email = user.Email,
                TenantId = request.TenantId,
                TenantRoles = request.TenantRoleTypes
            }, CancellationToken.None);
            await context.SaveChangesAsync(cancellationToken);

            return new TenantUserDetailsResponse
            {
                Id = user.Id,
                Email = user.Email,
                TenantRoles = user.TenantUsers.Single(x => x.TenantId == request.TenantId).TenantUserRoles
                    .Select(x => x.TenantRole)
            };
        }
    }
}