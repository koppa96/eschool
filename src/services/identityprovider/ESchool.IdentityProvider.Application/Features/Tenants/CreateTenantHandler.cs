using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Domain.Entities;
using ESchool.IdentityProvider.Interface.Features.Tenants;
using ESchool.IdentityProvider.Interface.IntegrationEvents.Tenants;
using ESchool.Libs.Outbox.Services;
using MediatR;

namespace ESchool.IdentityProvider.Application.Features.Tenants
{
    public class CreateTenantHandler : IRequestHandler<CreateTenantCommand, TenantDetailsResponse>
    {
        private readonly IdentityProviderContext context;
        private readonly IMapper mapper;
        private readonly IEventPublisher publisher;

        public CreateTenantHandler(IdentityProviderContext context,
            IMapper mapper,
            IEventPublisher publisher)
        {
            this.context = context;
            this.mapper = mapper;
            this.publisher = publisher;
        }
        
        public async Task<TenantDetailsResponse> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            var tenant = mapper.Map<Tenant>(request);
            context.Tenants.Add(tenant);
            
            publisher.Setup(context);
            await publisher.PublishAsync(new TenantCreatedOrUpdatedEvent
            {
                TenantId = tenant.Id,
                Name = tenant.Name,
                OmIdentifier = tenant.OmIdentifier
            }, cancellationToken);
            
            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<TenantDetailsResponse>(tenant);
        }
    }
}
