using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Application.Features.Tenants;
using ESchool.IdentityProvider.Application.Features.Tenants.Common;
using ESchool.Libs.Application.Cqrs.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.IdentityProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        private readonly IMediator mediator;

        public TenantsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<PagedListResponse<TenantListResponse>> GetTenants([FromQuery] TenantListQuery query, CancellationToken cancellationToken)
        {
            return mediator.Send(query, cancellationToken);
        }

        [HttpGet("{id}")]
        public Task<TenantDetailsResponse> GetTenant(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult<TenantDetailsResponse>> CreateTenant([FromBody] CreateTenantCommand command, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetTenant), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public Task<TenantDetailsResponse> UpdateTenant([FromBody] EditTenantCommand command, CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{id}")]
        public Task DeleteTenant(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new DeleteTenantCommand { TenantId = id }, cancellationToken);
        }
    }
}