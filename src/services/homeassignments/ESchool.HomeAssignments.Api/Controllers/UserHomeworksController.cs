using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Interface.Features.UserHomeworks;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Interface.Query;
using ESchool.Libs.Interface.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.HomeAssignments.Api.Controllers
{
    [Route("api/school-years/{schoolYearId}")]
    public class UserHomeworksController : ESchoolControllerBase
    {
        private readonly IMediator mediator;

        public UserHomeworksController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(PolicyNames.Student)]
        [HttpGet("subjects/{subjectId}/homeworks")]
        public Task<PagedListResponse<StudentHomeworkListResponse>> ListStudentHomeworks(
            Guid schoolYearId,
            Guid subjectId,
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize,
            [FromQuery] bool expired,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new StudentHomeworkListQuery
            {
                SchoolYearId = schoolYearId,
                SubjectId = subjectId,
                PageIndex = pageIndex,
                PageSize = pageSize == 0 ? PagedListQuery.DefaultPageSize : pageSize,
                Expired = expired
            }, cancellationToken);
        }

        [Authorize(PolicyNames.Teacher)]
        [HttpGet("classes/{classId}/subjects/{subjectId}/homeworks")]
        public Task<PagedListResponse<TeacherHomeworkListResponse>> ListTeacherHomeworks(
            Guid schoolYearId,
            Guid classId,
            Guid subjectId,
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize,
            [FromQuery] bool includeReviewed,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new TeacherHomeworkListQuery
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