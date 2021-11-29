using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Interface.Features.Tenants;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Interface.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.IdentityProvider.Controllers
{
    [Route("api/[controller]")]
    public class TenantsController : ESchoolControllerBase
    {
        private readonly IMediator mediator;

        public TenantsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Authorize(nameof(GlobalRoleType.TenantAdministrator))]
        public Task<PagedListResponse<TenantListResponse>> GetTenants([FromQuery] TenantListQuery query, CancellationToken cancellationToken)
        {
            return mediator.Send(query, cancellationToken);
        }

        [HttpGet("{id}")]
        [Authorize(PolicyNames.AdministratorOrTenantAdministrator)]
        public Task<TenantDetailsResponse> GetTenant(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new GetTenantQuery
            {
                TenantId = id
            }, cancellationToken);
        }

        [HttpPost]
        [Authorize(nameof(GlobalRoleType.TenantAdministrator))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<TenantDetailsResponse>> CreateTenant([FromBody] CreateTenantCommand command, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetTenant), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        [Authorize(PolicyNames.AdministratorOrTenantAdministrator)]
        public Task<TenantDetailsResponse> UpdateTenant([FromBody] EditTenantCommand command, CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{id}")]
        [Authorize(nameof(GlobalRoleType.TenantAdministrator))]
        public Task DeleteTenant(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new DeleteTenantCommand { TenantId = id }, cancellationToken);
        }
    }
}