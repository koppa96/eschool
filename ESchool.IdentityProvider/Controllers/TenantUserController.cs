using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Application.Features.TenantUsers;
using ESchool.IdentityProvider.Application.Features.Users.Common;
using ESchool.Libs.Application.Cqrs.Query;
using ESchool.Libs.Application.Cqrs.Response;
using ESchool.Libs.AspNetCore.Filters.GlobalRole;
using ESchool.Libs.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.IdentityProvider.Controllers
{
    [Authorize("Default")]
    [GlobalRoleFilter(GlobalRoleType.TenantAdministrator)]
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
        public Task<PagedListResponse<UserListResponse>> GetTenantUsers(Guid tenantId, [FromQuery] int pageSize, [FromQuery] int pageIndex, CancellationToken cancellationToken)
        {
            return mediator.Send(new TenantUserListQuery
            {
                TenantId = tenantId,
                PageIndex = pageIndex,
                PageSize = pageSize == 0 ? 25 : pageSize
            }, cancellationToken);
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

        [HttpDelete("{userId}")]
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