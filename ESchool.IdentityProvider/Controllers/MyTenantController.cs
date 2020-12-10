using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Application.Features.Tenants;
using ESchool.IdentityProvider.Application.Features.Tenants.Common;
using ESchool.Libs.AspNetCore.Filters.TenantRole;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.IdentityProvider.Controllers
{
    [Authorize("Default")]
    [TenantRoleFilter(TenantRoleType.Administrator)]
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
            return mediator.Send(new GetTenantQuery { TenantId = identityService.GetTenantId() });
        }
        
        [HttpPut]
        public Task<TenantDetailsResponse> UpdateTenant([FromBody] EditTenantCommand command, CancellationToken cancellationToken)
        {
            if (identityService.GetTenantId() != command.Id)
            {
                throw new InvalidOperationException("You can not edit the identifier of your tenant.");
            }
            
            return mediator.Send(command, cancellationToken);
        }

    }
}