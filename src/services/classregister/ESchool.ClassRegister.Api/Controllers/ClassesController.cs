using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.Classes;
using ESchool.ClassRegister.Interface.Features.Classes;
using ESchool.ClassRegister.Interface.Features.Users.Students;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Interface.Commands;
using ESchool.Libs.Interface.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers
{
    [Authorize(PolicyNames.Administrator)]
    [ApiController]
    [Route("api/classes")]
    public class ClassesController : ControllerBase
    {
        private readonly IMediator mediator;

        public ClassesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<PagedListResponse<ClassListResponse>> ListClasses(
            [FromQuery] ClassListQuery query, CancellationToken cancellationToken)
        {
            return mediator.Send(query, cancellationToken);
        }

        [HttpGet("{id}")]
        public Task<ClassDetailsResponse> GetClass(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new ClassGetQuery
            {
                Id = id
            }, cancellationToken);
        }

        [HttpPost]
        public Task<ClassDetailsResponse> CreateClass([FromBody] ClassCreateCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [HttpPost("{classId}/students/{studentId}")]
        public Task AssignStudent(Guid classId, Guid studentId, CancellationToken cancellationToken)
        {
            return mediator.Send(new AssignStudentToClassCommand
            {
                ClassId = classId,
                StudentId = studentId
            }, cancellationToken);
        }

        [HttpDelete("{classId}/students/{studentId}")]
        public Task RemoveStudent(Guid classId, Guid studentId, CancellationToken cancellationToken)
        {
            return mediator.Send(new RemoveStudentFromClassCommand
            {
                StudentId = studentId
            }, cancellationToken);
        }

        [HttpPut("{id}")]
        public Task<ClassDetailsResponse> EditClass(Guid id, [FromBody] ClassEditCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new EditCommand<ClassEditCommand, ClassDetailsResponse>
            {
                Id = id,
                InnerCommand = command
            }, cancellationToken);
        }

        [HttpDelete("{id}")]
        public Task DeleteClass(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new ClassDeleteCommand
            {
                Id = id
            }, cancellationToken);
        }
    }
}