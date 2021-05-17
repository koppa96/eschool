using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Application.Cqrs.Query;
using ESchool.Libs.Application.Cqrs.Response;
using ESchool.Libs.Domain.Enums;
using ESchool.Testing.Application.Features.Tests;
using ESchool.Testing.Application.Features.Tests.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Testing.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserTestsController : ControllerBase
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