using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Interface.IntegrationEvents.TenantUsers;
using ESchool.Libs.Outbox.Services;
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
        private readonly IEventPublisher publisher;

        public TenantUserDeleteHandler(IdentityProviderContext context, IEventPublisher publisher,
            IMapper mapper)
        {
            this.context = context;
            this.publisher = publisher;
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
                await publisher.PublishAsync(new TenantUserDeletedEvent
                {
                    UserId = tenantUser.UserId,
                    TenantId = tenantUser.TenantId
                }, CancellationToken.None);
            }

            return Unit.Value;
        }
    }
}