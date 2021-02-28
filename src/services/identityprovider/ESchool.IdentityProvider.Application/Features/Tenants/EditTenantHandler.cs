using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.IdentityProvider.Application.Features.Tenants.Common;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Domain.Entities;
using ESchool.IdentityProvider.Interface.IntegrationEvents.Tenants;
using MassTransit;
using MediatR;

namespace ESchool.IdentityProvider.Application.Features.Tenants
{
    public class EditTenantCommand : IRequest<TenantDetailsResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string OfficialEmailAddress { get; set; }
        public string OmIdentifier { get; set; }
        public string HeadMaster { get; set; }
    }
    
    public class EditTenantHandler : IRequestHandler<EditTenantCommand, TenantDetailsResponse>
    {
        private readonly IdentityProviderContext context;
        private readonly IMapper mapper;
        private readonly IPublishEndpoint publishEndpoint;

        public EditTenantHandler(IdentityProviderContext context, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            this.context = context;
            this.mapper = mapper;
            this.publishEndpoint = publishEndpoint;
        }
        
        public async Task<TenantDetailsResponse> Handle(EditTenantCommand request, CancellationToken cancellationToken)
        {
            var tenant = mapper.Map<Tenant>(request);
            context.Tenants.Update(tenant);
            await context.SaveChangesAsync(cancellationToken);
            await publishEndpoint.Publish(new TenantCreatedOrUpdatedEvent
            {
                TenantId = tenant.Id
            }, CancellationToken.None);
            
            return mapper.Map<TenantDetailsResponse>(tenant);
        }
    }
}