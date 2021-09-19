using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Interface.Features.Users;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Domain.Services;
using ESchool.Libs.Interface.Commands;
using ESchool.Libs.Interface.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.IdentityProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IIdentityService identityService;

        public UsersController(IMediator mediator, IIdentityService identityService)
        {
            this.mediator = mediator;
            this.identityService = identityService;
        }

        [HttpGet("search")]
        [Authorize(PolicyNames.AdministratorOrTenantAdministrator)]
        public Task<List<UserListResponse>> SearchUsers(string name, CancellationToken cancellationToken)
        {
            var query = new UserSearchQuery { Name = name };
            return mediator.Send(query, cancellationToken);
        }

        [HttpGet]
        [Authorize(PolicyNames.TenantAdministrator)]
        public Task<PagedListResponse<UserListResponse>> ListUsers([FromQuery] UserListQuery query,
            CancellationToken cancellationToken)
        {
            return mediator.Send(query, cancellationToken);
        }
        
        [HttpGet("{id}")]
        [Authorize(PolicyNames.TenantAdministrator)]
        public Task<UserDetailsResponse> GetUser(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new UserGetQuery { Id = id }, cancellationToken);
        }

        [HttpPost]
        [Authorize(PolicyNames.TenantAdministrator)]
        public Task<UserDetailsResponse> CreateUser([FromBody] UserCreateCommand command, CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [HttpPut("{id}")]
        [Authorize(PolicyNames.TenantAdministrator)]
        public Task<UserDetailsResponse> EditUser(Guid id, [FromBody] UserEditCommand command,
            CancellationToken cancellationToken)
        {
            var editCommand = new EditCommand<UserEditCommand, UserDetailsResponse>
            {
                Id = id,
                InnerCommand = command
            };

            return mediator.Send(editCommand, cancellationToken);
        }

        [HttpDelete("{id}")]
        [Authorize(PolicyNames.TenantAdministrator)]
        public Task DeleteUser(Guid id, CancellationToken cancellationToken)
        {
            var command = new UserDeleteCommand
            {
                Id = id
            };
            
            return mediator.Send(command, cancellationToken);
        }

        [HttpGet("me")]
        [Authorize(PolicyNames.AnyRole)]
        public Task<UserDetailsResponse> GetMe(CancellationToken cancellationToken)
        {
            return mediator.Send(new UserGetQuery { Id = identityService.GetCurrentUserId() }, cancellationToken);
        }
    }
}
