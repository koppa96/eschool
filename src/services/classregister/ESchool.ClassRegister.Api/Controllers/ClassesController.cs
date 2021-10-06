using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.Classes;
using ESchool.ClassRegister.Interface.Features.Classes;
using ESchool.ClassRegister.Interface.Features.Users.Students;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Interface.Commands;
using ESchool.Libs.Interface.Query;
using ESchool.Libs.Interface.Response;
using ESchool.Libs.Interface.Response.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers
{
    [Route("api/classes")]
    public class ClassesController : ESchoolControllerBase
    {
        private readonly IMediator mediator;

        public ClassesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Authorize(PolicyNames.TeacherOrAdministrator)]
        public Task<PagedListResponse<ClassListResponse>> ListClasses(
            [FromQuery] ClassListQuery query, CancellationToken cancellationToken)
        {
            return mediator.Send(query, cancellationToken);
        }

        [HttpGet("{id}")]
        [Authorize(PolicyNames.TeacherOrAdministrator)]
        public Task<ClassDetailsResponse> GetClass(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new ClassGetQuery
            {
                Id = id
            }, cancellationToken);
        }

        [HttpGet("{id}/students")]
        [Authorize(PolicyNames.TeacherOrAdministrator)]
        public Task<PagedListResponse<UserRoleListResponse>> ListStudents(Guid id, string searchText, [FromQuery] PagedListQuery query, CancellationToken cancellationToken)
        {
            return mediator.Send(query.ToTypedQuery<ClassStudentListQuery>(x =>
            {
                x.ClassId = id;
                x.SearchText = searchText;
            }), cancellationToken);
        }

        [HttpPost]
        [Authorize(PolicyNames.Administrator)]
        public Task<ClassDetailsResponse> CreateClass([FromBody] ClassCreateCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [HttpPost("{classId}/students/{studentId}")]
        [Authorize(PolicyNames.Administrator)]
        public Task AssignStudent(Guid classId, Guid studentId, CancellationToken cancellationToken)
        {
            return mediator.Send(new AssignStudentToClassCommand
            {
                ClassId = classId,
                StudentId = studentId
            }, cancellationToken);
        }

        [HttpDelete("{classId}/students/{studentId}")]
        [Authorize(PolicyNames.Administrator)]
        public Task RemoveStudent(Guid classId, Guid studentId, CancellationToken cancellationToken)
        {
            return mediator.Send(new RemoveStudentFromClassCommand
            {
                StudentId = studentId
            }, cancellationToken);
        }

        [HttpPut("{id}")]
        [Authorize(PolicyNames.Administrator)]
        public Task<ClassDetailsResponse> EditClass(Guid id, [FromBody] ClassEditCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new EditCommand<ClassEditCommand, ClassDetailsResponse>
            {
                Id = id,
                InnerCommand = command
            }, cancellationToken);
        }

        [HttpPatch("{id}/close")]
        [Authorize(PolicyNames.Administrator)]
        public Task<ClassDetailsResponse> CloseClass(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new ClassFinishCommand
            {
                ClassId = id
            }, cancellationToken);
        }

        [HttpDelete("{id}")]
        [Authorize(PolicyNames.Administrator)]
        public Task DeleteClass(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new ClassDeleteCommand
            {
                Id = id
            }, cancellationToken);
        }
    }
}