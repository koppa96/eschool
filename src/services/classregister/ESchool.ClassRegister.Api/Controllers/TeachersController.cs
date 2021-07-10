using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.Features.Users;
using ESchool.ClassRegister.Interface.Features.Users.Teachers;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Interface.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers
{
    [Authorize(PolicyNames.Administrator)]
    [Route("api/teachers")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly IMediator mediator;

        public TeachersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<PagedListResponse<UserRoleListResponse>> ListTeachers([FromQuery] TeacherListQuery query, CancellationToken cancellationToken)
        {
            return mediator.Send(query, cancellationToken);
        }
    }
}