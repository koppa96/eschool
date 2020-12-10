using ESchool.IdentityProvider.Application.Features.Users;
using ESchool.IdentityProvider.Application.Features.Users.Common;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.AspNetCore.Filters.GlobalRole;
using ESchool.Libs.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace ESchool.IdentityProvider.Controllers
{
    [Authorize("Default")]
    [GlobalRoleFilter(GlobalRoleType.TenantAdministrator)]
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

        [HttpPost]
        public Task<UserDetailsResponse> CreateUser([FromBody] UserCreateCommand command, CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [HttpGet("{id}")]
        public Task<UserGetResponse> GetUser(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new UserGetQuery { Id = id }, cancellationToken);
        }

        [HttpGet("me")]
        public Task<UserGetResponse> GetMe(CancellationToken cancellationToken)
        {
            return mediator.Send(new UserGetQuery { Id = identityService.GetCurrentUserId() }, cancellationToken);
        }
    }
}
