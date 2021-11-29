using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.Grading.Grades;
using ESchool.ClassRegister.Interface.Features.Grading.Grades;
using ESchool.Libs.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers.SubjectManagement
{
    [Route("api/school-years/{schoolYearId}/classes/{classId}/subjects/{subjectId}/students/{studentId}/grades")]
    public class ClassSchoolYearSubjectStudentGradesController : ESchoolControllerBase
    {
        private readonly IMediator mediator;

        public ClassSchoolYearSubjectStudentGradesController(IMediator mediator)
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
                WrittenIn = dto.WrittenIn,
                Value = dto.Value
            }, cancellationToken);
        }
    }
}