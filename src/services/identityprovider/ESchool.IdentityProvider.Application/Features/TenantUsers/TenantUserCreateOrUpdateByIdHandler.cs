using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Domain.Entities.Users;
using ESchool.IdentityProvider.Interface.Features.TenantUsers;
using ESchool.IdentityProvider.Interface.IntegrationEvents.TenantUsers;
using ESchool.Libs.Outbox.Services;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.IdentityProvider.Application.Features.TenantUsers
{
    public class TenantUserCreateOrUpdateByIdHandler : IRequestHandler<TenantUserCreateOrUpdateByIdCommand, TenantUserDetailsResponse>
    {
        private readonly IdentityProviderContext context;
        private readonly IEventPublisher publisher;

        public TenantUserCreateOrUpdateByIdHandler(IdentityProviderContext context, IEventPublisher publisher)
        {
            this.context = context;
            this.publisher = publisher;
        }

        public async Task<TenantUserDetailsResponse> Handle(TenantUserCreateOrUpdateByIdCommand request,
            CancellationToken cancellationToken)
        {
            var user = await context.Users.Include(x => x.TenantUsers)
                .ThenInclude(x => x.TenantUserRoles)
                .SingleAsync(x => x.Id == request.UserId, cancellationToken);

            var existingTenantUser = user.TenantUsers.SingleOrDefault(x => x.TenantId == request.TenantId);
            if (existingTenantUser == null)
            {
                context.TenantUsers.Add(new TenantUser
                {
                    TenantId = request.TenantId,
                    UserId = user.Id,
                    TenantUserRoles = request.TenantRoleTypes.Select(x => new TenantUserRole
                    {
                        TenantRole = x
                    }).ToList()
                });
            }
            else
            {
                context.TenantUserRoles.RemoveRange(existingTenantUser.TenantUserRoles);
                context.TenantUserRoles.AddRange(request.TenantRoleTypes.Select(x => new TenantUserRole
                {
                    TenantUser = existingTenantUser,
                    TenantRole = x
                }));
            }
            
            publisher.Setup(context);
            await publisher.PublishAsync(new TenantUserCreatedOrEditedEvent
            {
                UserId = user.Id,
                Name = user.Name,
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