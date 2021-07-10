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
    public class EditTenantHandler : IRequestHandler<EditTenantCommand, TenantDetailsResponse>
    {
        private readonly IdentityProviderContext context;
        private readonly IMapper mapper;
        private readonly IEventPublisher publisher;

        public EditTenantHandler(IdentityProviderContext context, IMapper mapper, IEventPublisher publisher)
        {
            this.context = context;
            this.mapper = mapper;
            this.publisher = publisher;
        }
        
        public async Task<TenantDetailsResponse> Handle(EditTenantCommand request, CancellationToken cancellationToken)
        {
            var tenant = mapper.Map<Tenant>(request);
            context.Tenants.Update(tenant);
            
            await publisher.PublishAsync(new TenantCreatedOrUpdatedEvent
            {
                TenantId = tenant.Id,
                Name = tenant.Name,
                OmIdentifier = tenant.OmIdentifier
            }, CancellationToken.None);
            await context.SaveChangesAsync(cancellationToken);

            return mapper.Map<TenantDetailsResponse>(tenant);
        }
    }
}