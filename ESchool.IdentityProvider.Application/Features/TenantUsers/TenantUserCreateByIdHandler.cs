using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Application.Features.TenantUsers.Common;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Domain.Entities.Users;
using ESchool.Libs.Application.IntegrationEvents.TenantUsers;
using ESchool.Libs.Domain.Enums;
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
        private readonly IPublishEndpoint publishEndpoint;

        public TenantUserCreateByIdHandler(IdentityProviderContext context, IPublishEndpoint publishEndpoint)
        {
            this.context = context;
            this.publishEndpoint = publishEndpoint;
        }
        
        public async Task<TenantUserDetailsResponse> Handle(TenantUserCreateByIdCommand request, CancellationToken cancellationToken)
        {
            var user = await context.Users.Include(x => x.TenantUsers)
                .ThenInclude(x => x.TenantUserRoles)
                .SingleAsync(x => x.Id == request.UserId, cancellationToken);

            context.TenantUsers.Add(new TenantUser
            {
                TenantId = request.TenantId,
                UserId = user.Id,
                TenantUserRoles = request.TenantRoleTypes.Select(x => new TenantUserRole
                {
                    TenantRole = x
                }).ToList()
            });

            await context.SaveChangesAsync(cancellationToken);
            await publishEndpoint.Publish(new TenantUserCreatedIntegrationEvent
            {
                UserId = user.Id,
                Email = user.Email,
                TenantId = request.TenantId,
                TenantRoleTypes = user.TenantUsers.Single(x => x.TenantId == request.TenantId).TenantUserRoles
                    .Select(x => x.TenantRole)
                    .ToList()
            }, CancellationToken.None);
            
            return new TenantUserDetailsResponse
            {
                Id = user.Id,
                Email = user.Email,
                TenantRoleTypes = user.TenantUsers.Single(x => x.TenantId == request.TenantId).TenantUserRoles
                    .Select(x => x.TenantRole)
            };
        }
    }
}