using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Api.Dtos;
using ESchool.ClassRegister.Application.Features.Grading.Grades;
using ESchool.ClassRegister.Application.Features.Grading.Grades.Common;
using ESchool.Libs.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers
{
    [ApiController]
    [Route("api/school-years/{schoolYearId}/classes/{classId}/subjects/{subjectId}/students/{studentId}/grades")]
    public class ClassSchoolYearSubjectGradesController : ControllerBase
    {
        private readonly IMediator mediator;

        public ClassSchoolYearSubjectGradesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(PolicyNames.Teacher)]
        [HttpPost]
        public Task<GradeDetailsResponse> CreateGrade(
            Guid schoolYearId,
            Guid classId,
            Guid subjectId,
            Guid studentId,
            [FromBody] ClassSchoolYearSubjectGradeCreateDto dto,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new GradeCreateCommand
            {
                SchoolYearId = schoolYearId,
                StudentId = studentId,
                SubjectId = subjectId,
                Description = dto.Description,
                GradeKindId = dto.GradeKindId,
                Value = dto.Value
            }, cancellationToken);
        }

        [Authorize(PolicyNames.TeacherOrAdministrator)]
        [HttpGet]
        public Task<List<GradeListByClassSchoolYearSubjectResponse>> ListGradesBySubject(
            Guid schoolYearId,
            Guid classId,
            Guid subjectId,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new GradeListByClassSchoolYearSubjectQuery
            {
                SchoolYearId = schoolYearId,
                ClassId = classId,
                SubjectId = subjectId
            }, cancellationToken);
        }
    }
}