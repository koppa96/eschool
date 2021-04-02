using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.IdentityProvider.Application.Features.Tenants.Common;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Domain.Entities;
using ESchool.IdentityProvider.Interface.IntegrationEvents.Tenants;
using ESchool.Libs.Outbox.Services;
using MassTransit;
using MediatR;

namespace ESchool.IdentityProvider.Application.Features.Tenants
{
    public class CreateTenantCommand : IRequest<TenantDetailsResponse>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string OfficialEmailAddress { get; set; }
        public string OmIdentifier { get; set; }
        public string HeadMaster { get; set; }
    }

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
            
            await context.SaveChangesAsync(cancellationToken);
            await publisher.PublishAsync(new TenantCreatedOrUpdatedEvent
            {
                TenantId = tenant.Id
            }, CancellationToken.None);
            
            return mapper.Map<TenantDetailsResponse>(tenant);
        }
    }
}
