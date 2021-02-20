using ESchool.IdentityProvider.Application.Features.Users;
using ESchool.IdentityProvider.Application.Features.Users.Common;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Application.Cqrs.Query;
using ESchool.Libs.Application.Cqrs.Response;
using ESchool.Libs.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

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

        [HttpGet("me")]
        [Authorize("Default")]
        public Task<UserDetailsResponse> GetMe(CancellationToken cancellationToken)
        {
            return mediator.Send(new UserGetQuery { Id = identityService.GetCurrentUserId() }, cancellationToken);
        }
    }
}
