using ESchool.IdentityProvider.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Interface.IntegrationEvents.Tenants;
using ESchool.Libs.Domain.Extensions;
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
        private readonly IPublishEndpoint publishEndpoint;

        public DeleteTenantHandler(IdentityProviderContext context, IPublishEndpoint publishEndpoint)
        {
            this.context = context;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(DeleteTenantCommand request, CancellationToken cancellationToken)
        {
            var tenant = await context.Tenants.FindOrThrowAsync(request.TenantId, cancellationToken);
            if (tenant != null)
            {
                context.Tenants.Remove(tenant);
                
                await context.SaveChangesAsync(cancellationToken);
                await publishEndpoint.Publish(new TenantDeletedEvent
                {
                    TenantId = tenant.Id
                }, cancellationToken);
            }

            return Unit.Value;
        }
    }
}
