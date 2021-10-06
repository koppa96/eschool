using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Interface.Features.TenantUsers;
using ESchool.IdentityProvider.Interface.Features.Users;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Interface.Query;
using ESchool.Libs.Interface.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.IdentityProvider.Controllers
{
    [Authorize(PolicyNames.AdministratorOrTenantAdministrator)]
    [Route("api/tenants/{tenantId}/users")]
    public class TenantUserController : ESchoolControllerBase
    {
        private readonly IMediator mediator;

        public TenantUserController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        [HttpGet]
        public Task<PagedListResponse<TenantUserListResponse>> GetTenantUsers(Guid tenantId, [FromQuery] int pageSize, [FromQuery] int pageIndex, CancellationToken cancellationToken)
        {
            return mediator.Send(new TenantUserListQuery
            {
                TenantId = tenantId,
                PageIndex = pageIndex,
                PageSize = pageSize == 0 ? PagedListQuery.DefaultPageSize : pageSize
            }, cancellationToken);
        }

        [HttpPut("{userId}")]
        public Task CreateOrUpdateTenantUser(Guid tenantId, Guid userId, [FromBody] List<TenantRoleType> tenantRoleTypes, CancellationToken cancellationToken)
        {
            return mediator.Send(new TenantUserCreateOrUpdateByIdCommand
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