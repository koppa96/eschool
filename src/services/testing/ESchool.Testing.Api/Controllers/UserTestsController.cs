using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Interface.Query;
using ESchool.Libs.Interface.Response;
using ESchool.Testing.Interface.Features.Tests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Testing.Api.Controllers
{
    [Route("api")]
    public class UserTestsController : ESchoolControllerBase
    {
        private readonly IMediator mediator;

        public UserTestsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        [HttpGet("teacher/school-years/{schoolYearId}/classes/{classId}/subjects/{subjectId}/tests")]
        [Authorize(nameof(TenantRoleType.Teacher))]
        public Task<PagedListResponse<TestListResponse>> ListTestsAsTeacherAsync(
            Guid schoolYearId,
            Guid classId,
            Guid subjectId,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new TestListAsTeacherQuery
            {
                SchoolYearId = schoolYearId,
                ClassId = classId,
                SubjectId = subjectId,
                PageIndex = pageIndex,
                PageSize = pageSize == default ? PagedListQuery.DefaultPageSize : pageSize
            }, cancellationToken);
        }

        [HttpGet("student/school-years/{schoolYearId}/classes/{classId}/subjects/{subjectId}/tests")]
        [Authorize(nameof(TenantRoleType.Student))]
        public Task<PagedListResponse<TestListResponse>> ListTestsAsStudentAsync(
            Guid schoolYearId,
            Guid classId,
            Guid subjectId,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new TestListAsStudentQuery
            {
                SchoolYearId = schoolYearId,
                ClassId = classId,
                SubjectId = subjectId,
                PageIndex = pageIndex,
                PageSize = pageSize == default ? PagedListQuery.DefaultPageSize : pageSize
            }, cancellationToken);
        }
    }
}