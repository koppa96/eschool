using System;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Application.Features.Tenants;
using Grpc.Core;
using MediatR;

namespace ESchool.IdentityProvider.Grpc
{
    public class TenantServiceImpl : TenantService.TenantServiceBase
    {
        private readonly IMediator mediator;

        public TenantServiceImpl(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        public override async Task<TenantDetailsResponse> GetTenantDetails(TenantDetailsRequest request, ServerCallContext context)
        {
            var response = await mediator.Send(new GetTenantQuery
            {
                TenantId = Guid.Parse(request.TenantId)
            }, context.CancellationToken);

            return new TenantDetailsResponse
            {
                TenantId = response.Id.ToString(),
                Name = response.Name,
                OmIdentifier = response.OmIdentifier
            };
        }
    }
}