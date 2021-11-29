using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.Users;
using ESchool.ClassRegister.Interface.Features.Users;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Interface.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers
{
    [Route("api/users")]
    [Authorize(PolicyNames.AnyRole)]
    public class UsersController : ESchoolControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<PagedListResponse<ClassRegisterUserListResponse>> ListUsers([FromQuery] UserListQuery query,
            CancellationToken cancellationToken)
        {
            return mediator.Send(query, cancellationToken);
        }
    }
}