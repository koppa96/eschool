using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.Grading.Grades;
using ESchool.ClassRegister.Application.Features.SubjectManagement.Absences;
using ESchool.Libs.Application.Cqrs.Query;
using ESchool.Libs.Application.Cqrs.Response;
using ESchool.Libs.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator mediator;

        public StudentsController(IMediator mediator)
        {
            this.mediator = mediator;
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