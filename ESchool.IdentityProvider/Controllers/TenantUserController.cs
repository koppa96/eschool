using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Application.Features.TenantUsers;
using ESchool.IdentityProvider.Application.Features.Users.Common;
using ESchool.Libs.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.IdentityProvider.Controllers
{
    [Route("api/tenants/{tenantId}/users")]
    [ApiController]
    public class TenantUserController : ControllerBase
    {
        private readonly IMediator mediator;

        public TenantUserController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        [HttpGet]
        public Task<List<UserListResponse>> GetTenantUsers(Guid tenantId, CancellationToken cancellationToken)
        {
            return mediator.Send(new TenantUserListQuery { TenantId = tenantId }, cancellationToken);
        }

        [HttpPut("{userId}/roles")]
        public Task CreateTenantUser(Guid tenantId, Guid userId, [FromBody] List<TenantRoleType> tenantRoleTypes, CancellationToken cancellationToken)
        {
            return mediator.Send(new TenantUserCreateOrUpdateCommand
            {
                TenantId = tenantId,
                UserId = userId,
                TenantRoleTypes = tenantRoleTypes
            }, cancellationToken);
        }

        [HttpDelete]
        public Task DeleteTenantUser(Guid tenantId, Guid userId, CancellationToken cancellationToken)
        {
            return mediator.Send(new TenantUserDeleteCommand
            {
                TenantId = tenantId,
                UserId = userId
            }, cancellationToken);
        }
    }
}