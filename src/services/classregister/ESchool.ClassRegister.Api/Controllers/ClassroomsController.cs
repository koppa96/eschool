using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.Features.Classrooms;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Interface.Commands;
using ESchool.Libs.Interface.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers
{
    [Authorize(PolicyNames.Administrator)]
    [Route("api/classrooms")]
    public class ClassroomsController : ESchoolControllerBase
    {
        private readonly IMediator mediator;

        public ClassroomsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<PagedListResponse<ClassroomListResponse>> ListClassrooms([FromQuery] ClassroomListQuery query, CancellationToken cancellationToken)
        {
            return mediator.Send(query, cancellationToken);
        }

        [HttpGet("{id}")]
        public Task<ClassroomDetailsResponse> GetClassroom(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new ClassroomGetQuery { Id = id }, cancellationToken);
        }

        [HttpPost]
        public Task<ClassroomDetailsResponse> CreateClassroom([FromBody] ClassroomCreateCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [HttpPut("{id}")]
        public Task<ClassroomDetailsResponse> EditClassroom(Guid id, [FromBody] ClassroomEditCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new EditCommand<ClassroomEditCommand, ClassroomDetailsResponse>
            {
                Id = id,
                InnerCommand = command
            }, cancellationToken);
        }

        [HttpDelete("{id}")]
        public Task DeleteClassroom(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new ClassroomDeleteCommand { Id = id }, cancellationToken);
        }
    }
}