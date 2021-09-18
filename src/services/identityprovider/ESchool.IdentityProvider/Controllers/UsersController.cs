using System;
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

        [HttpGet]
        public Task<PagedListResponse<UserListResponse>> ListUsers([FromQuery] UserListQuery query,
            CancellationToken cancellationToken)
        {
            return mediator.Send(query, cancellationToken);
        }
        
        [HttpGet("{id}")]
        public Task<UserDetailsResponse> GetUser(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new UserGetQuery { Id = id }, cancellationToken);
        }

        [HttpPost]
        public Task<UserDetailsResponse> CreateUser([FromBody] UserCreateCommand command, CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [HttpPut("{id}")]
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
