using ESchool.IdentityProvider.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Interface.IntegrationEvents.Tenants;
using ESchool.Libs.Domain.Extensions;
using ESchool.Libs.Outbox.Services;
using MassTransit;

namespace ESchool.IdentityProvider.Application.Features.Tenants
{
    public class DeleteTenantCommand : IRequest
    {
        public Guid TenantId { get; set; }
    }

    public class DeleteTenantHandler : IRequestHandler<DeleteTenantCommand>
    {
        private readonly IdentityProviderContext context;
        private readonly IEventPublisher publisher;

        public DeleteTenantHandler(IdentityProviderContext context, IEventPublisher publisher)
        {
            this.context = context;
            this.publisher = publisher;
        }

        public async Task<Unit> Handle(DeleteTenantCommand request, CancellationToken cancellationToken)
        {
            var tenant = await context.Tenants.FindOrThrowAsync(request.TenantId, cancellationToken);
            if (tenant != null)
            {
                context.Tenants.Remove(tenant);
                
                await publisher.PublishAsync(new TenantDeletedEvent
                {
                    TenantId = tenant.Id
                }, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
