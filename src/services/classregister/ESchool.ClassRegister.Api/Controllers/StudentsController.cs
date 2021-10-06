using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.SubjectManagement.Absences;
using ESchool.ClassRegister.Interface.Features.Grading.Grades;
using ESchool.ClassRegister.Interface.Features.Users;
using ESchool.ClassRegister.Interface.Features.Users.Students;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Interface.Query;
using ESchool.Libs.Interface.Response;
using ESchool.Libs.Interface.Response.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers
{
    [Route("api/students")]
    public class StudentsController : ESchoolControllerBase
    {
        private readonly IMediator mediator;

        public StudentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("unassigned")]
        [Authorize(PolicyNames.Administrator)]
        public Task<PagedListResponse<UserRoleListResponse>> ListUnassignedStudents(
            [FromQuery] StudentsWithoutClassListQuery query, CancellationToken cancellationToken)
        {
            return mediator.Send(query, cancellationToken);
        }

        [HttpGet("{studentId}/absences")]
        [Authorize(PolicyNames.AnyRole)]
        public Task<PagedListResponse<AbsenceListResponse>> ListAbsences(Guid studentId,
            [FromQuery] Guid schoolYearId,
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize,
            CancellationToken cancellationToken)
        {
            if (schoolYearId == default)
            {
                throw new ArgumentOutOfRangeException(nameof(schoolYearId), "A tanév megadása kötelező!");
            }

            return mediator.Send(new AbsenceListQuery
            {
                StudentId = studentId,
                SchoolYearId = schoolYearId,
                PageIndex = pageIndex,
                PageSize = pageSize == 0 ? PagedListQuery.DefaultPageSize : pageSize
            }, cancellationToken);
        }

        [HttpGet("{studentId}/grades")]
        public Task<List<GradeListByStudentResponse>> ListGrades(
            Guid studentId,
            [FromQuery] Guid schoolYearId,
            CancellationToken cancellationToken)
        {
            if (schoolYearId == default)
            {
                throw new ArgumentOutOfRangeException(nameof(schoolYearId), "A tanév megadása kötelező.");
            }
            
            return mediator.Send(new GradeListByStudentQuery
            {
                StudentId = studentId,
                SchoolYearId = schoolYearId
            }, cancellationToken);
        }
    }
}