using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Interface.IntegrationEvents.TenantUsers;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.IdentityProvider.Application.Features.TenantUsers
{
    public class TenantUserDeleteCommand : IRequest
    {
        public Guid TenantId { get; set; }
        public Guid UserId { get; set; }
    }

    public class TenantUserDeleteHandler : IRequestHandler<TenantUserDeleteCommand, Unit>
    {
        private readonly IdentityProviderContext context;
        private readonly IMapper mapper;
        private readonly IPublishEndpoint publishEndpoint;

        public TenantUserDeleteHandler(IdentityProviderContext context, IPublishEndpoint publishEndpoint,
            IMapper mapper)
        {
            this.context = context;
            this.publishEndpoint = publishEndpoint;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(TenantUserDeleteCommand request, CancellationToken cancellationToken)
        {
            var tenantUser = await context.TenantUsers.Include(x => x.User)
                    .ThenInclude(x => x.TenantUsers)
                        .ThenInclude(x => x.TenantUserRoles)
                .SingleOrDefaultAsync(x => x.UserId == request.UserId && x.TenantId == request.TenantId,
                    cancellationToken);

            if (tenantUser != null)
            {
                context.TenantUsers.Remove(tenantUser);
                await context.SaveChangesAsync(cancellationToken);
                await publishEndpoint.Publish(new TenantUserDeletedIntegrationEvent
                {
                    UserId = tenantUser.UserId,
                    TenantId = tenantUser.TenantId
                }, CancellationToken.None);
            }

            return Unit.Value;
        }
    }
}