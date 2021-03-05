using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.SubjectManagement.SubjectTeachers;
using ESchool.ClassRegister.Application.Features.Subjects;
using ESchool.ClassRegister.Application.Features.Subjects.Common;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Application.Cqrs.Response;
using ESchool.Libs.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers
{
    [Authorize(nameof(TenantRoleType.Administrator))]
    [ApiController]
    [Route("api/subjects")]
    public class SubjectsController : ControllerBase
    {
        private readonly IMediator mediator;

        public SubjectsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<PagedListResponse<SubjectListResponse>> ListSubjects([FromQuery] SubjectListQuery query, CancellationToken cancellationToken)
        {
            return mediator.Send(query, cancellationToken);
        }

        [HttpGet("{subjectId}")]
        public Task<SubjectDetailsResponse> GetSubject(Guid subjectId, CancellationToken cancellationToken)
        {
            return mediator.Send(new SubjectGetQuery { Id = subjectId }, cancellationToken);
        }

        [HttpPost]
        public Task<SubjectDetailsResponse> CreateSubject([FromBody] SubjectCreateCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [HttpPost("{subjectId}/teachers/{teacherId}")]
        public Task AssignTeacherToSubject(Guid subjectId, Guid teacherId, CancellationToken cancellationToken)
        {
            return mediator.Send(new SubjectTeacherCreateCommand
            {
                SubjectId = subjectId,
                TeacherId = teacherId
            }, cancellationToken);
        }

        [HttpPut("{subjectId}")]
        public Task<SubjectDetailsResponse> EditSubject(Guid subjectId, [FromBody] SubjectEditCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new EditCommand<SubjectEditCommand, SubjectDetailsResponse>
            {
                Id = subjectId,
                InnerCommand = command
            }, cancellationToken);
        }

        [HttpDelete("{id}")]
        public Task DeleteSubject(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new SubjectDeleteCommand { Id = id }, cancellationToken);
        }

        [HttpDelete("{subjectId}/teachers/{teacherId}")]
        public Task UnassignTeacherFromSubject(Guid subjectId, Guid teacherId, CancellationToken cancellationToken)
        {
            return mediator.Send(new SubjectTeacherDeleteCommand
            {
                SubjectId = subjectId,
                TeacherId = teacherId
            }, cancellationToken);
        }
    }
}