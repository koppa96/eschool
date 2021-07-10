﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Interface.Features.TenantUsers;
using ESchool.IdentityProvider.Interface.Features.Users;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Interface.Query;
using ESchool.Libs.Interface.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.IdentityProvider.Controllers
{
    [Authorize(nameof(GlobalRoleType.TenantAdministrator))]
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
                PageSize = pageSize == 0 ? PagedListQuery.DefaultPageSize : pageSize
            }, cancellationToken);
        }

        [HttpPost("{userId}")]
        public Task CreateTenantUser(Guid tenantId, Guid userId, [FromBody] List<TenantRoleType> tenantRoleTypes, CancellationToken cancellationToken)
        {
            return mediator.Send(new TenantUserCreateByIdCommand
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