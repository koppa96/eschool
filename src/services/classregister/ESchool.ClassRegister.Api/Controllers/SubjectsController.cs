using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.SubjectTeachers;
using ESchool.ClassRegister.Interface.Features.Subjects;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Interface.Commands;
using ESchool.Libs.Interface.Response;
using ESchool.Libs.Interface.Response.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers
{
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
        [Authorize(PolicyNames.AnyRole)]
        public Task<PagedListResponse<SubjectListResponse>> ListSubjects([FromQuery] SubjectListQuery query, CancellationToken cancellationToken)
        {
            return mediator.Send(query, cancellationToken);
        }

        [HttpGet("{subjectId}")]
        [Authorize(PolicyNames.AnyRole)]
        public Task<SubjectDetailsResponse> GetSubject(Guid subjectId, CancellationToken cancellationToken)
        {
            return mediator.Send(new SubjectGetQuery { Id = subjectId }, cancellationToken);
        }

        [HttpPost]
        [Authorize(PolicyNames.Administrator)]
        public Task<SubjectDetailsResponse> CreateSubject([FromBody] SubjectCreateCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [HttpGet("{subjectId}/teachers")]
        [Authorize(PolicyNames.AnyRole)]
        public Task<PagedListResponse<UserRoleListResponse>> GetTeachers(Guid subjectId,
            int pageSize,
            int pageIndex,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new SubjectTeacherListQuery
            {
                PageSize = pageSize == 0 ? 25 : pageSize,
                PageIndex = pageIndex,
                SubjectId = subjectId
            }, cancellationToken);
        }

        [HttpGet("{subjectId}/teachers/search")]
        [Authorize(PolicyNames.AnyRole)]
        public Task<List<UserRoleListResponse>> SearchSubjectTeachers(Guid subjectId,
            string searchText,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new SubjectTeacherSearchQuery
            {
                SubjectId = subjectId,
                SearchText = searchText
            }, cancellationToken);
        }

        [HttpPost("{subjectId}/teachers/{teacherId}")]
        [Authorize(PolicyNames.Administrator)]
        public Task AssignTeacherToSubject(Guid subjectId, Guid teacherId, CancellationToken cancellationToken)
        {
            return mediator.Send(new SubjectTeacherCreateCommand
            {
                SubjectId = subjectId,
                TeacherId = teacherId
            }, cancellationToken);
        }

        [HttpPut("{subjectId}")]
        [Authorize(PolicyNames.Administrator)]
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
        [Authorize(PolicyNames.Administrator)]
        public Task DeleteSubject(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new SubjectDeleteCommand { Id = id }, cancellationToken);
        }

        [HttpDelete("{subjectId}/teachers/{teacherId}")]
        [Authorize(PolicyNames.Administrator)]
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