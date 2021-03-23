using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Application.Features.Homeworks;
using ESchool.Libs.Application.Cqrs.Query;
using ESchool.Libs.Application.Cqrs.Response;
using ESchool.Libs.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.HomeAssignments.Api.Controllers
{
    [ApiController]
    public class UserHomeworksController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserHomeworksController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(PolicyNames.Student)]
        [HttpGet("api/student/school-years/{schoolYearId}/subjects/{subjectId}/homeworks")]
        public Task<PagedListResponse<HomeworkListAsStudentResponse>> ListStudentHomeworks(
            Guid schoolYearId,
            Guid subjectId,
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize,
            [FromQuery] bool expired,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new HomeworkListAsStudentQuery
            {
                SchoolYearId = schoolYearId,
                SubjectId = subjectId,
                PageIndex = pageIndex,
                PageSize = pageSize == 0 ? PagedListQuery.DefaultPageSize : pageSize,
                Expired = expired
            }, cancellationToken);
        }

        [Authorize(PolicyNames.Teacher)]
        [HttpGet("api/teacher/school-years/{schoolYearId}/classes/{classId}/subjects/{subjectId}/homeworks")]
        public Task<PagedListResponse<HomeworkListAsTeacherResponse>> ListTeacherHomeworks(
            Guid schoolYearId,
            Guid classId,
            Guid subjectId,
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize,
            [FromQuery] bool includeReviewed,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new HomeworkListAsTeacherQuery
            {
                SchoolYearId = schoolYearId,
                ClassId = classId,
                SubjectId = subjectId,
                PageIndex = pageIndex,
                PageSize = pageSize == 0 ? PagedListQuery.DefaultPageSize : pageSize,
                IncludeReviewed = includeReviewed
            }, cancellationToken);
        }
    }
}