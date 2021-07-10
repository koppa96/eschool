using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Interface.Features.Tenants;
using ESchool.IdentityProvider.Interface.Features.TenantUsers;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;
using ESchool.Libs.Interface.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.IdentityProvider.Controllers
{
    [Authorize(nameof(TenantRoleType.Administrator))]
    [Route("api/tenants/mine")]
    [ApiController]
    public class MyTenantController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IIdentityService identityService;

        public MyTenantController(IMediator mediator, IIdentityService identityService)
        {
            this.mediator = mediator;
            this.identityService = identityService;
        }

        [HttpGet]
        public Task<TenantDetailsResponse> GetTenant(CancellationToken cancellationToken)
        {
            return mediator.Send(new GetTenantQuery { TenantId = identityService.TryGetTenantId()!.Value });
        }

        [HttpPost("users")]
        public Task<TenantUserDetailsResponse> CreateTenantUser([FromBody] TenantUserCreateByEmailCommand byEmailCommand, CancellationToken cancellation)
        {
            return mediator.Send(byEmailCommand, cancellation);
        }

        [HttpPut("users/{userId}")]
        public Task<TenantUserDetailsResponse> EditTenantUser(Guid userId, [FromBody] TenantUserEditCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new EditCommand<TenantUserEditCommand, TenantUserDetailsResponse>
            {
                Id = userId,
                InnerCommand = command
            });
        }
        
        [HttpPut]
        public Task<TenantDetailsResponse> UpdateTenant([FromBody] EditTenantCommand command, CancellationToken cancellationToken)
        {
            if (identityService.TryGetTenantId() != command.Id)
            {
                throw new InvalidOperationException("You can not edit the identifier of your tenant.");
            }
            
            return mediator.Send(command, cancellationToken);
        }

        [HttpDelete("users/{userId}")]
        public Task DeleteTenantUser(Guid userId, CancellationToken cancellationToken)
        {
            return mediator.Send(new TenantUserDeleteCommand
            {
                UserId = userId,
                TenantId = identityService.GetTenantId()
            }, cancellationToken);
        }
    }
}