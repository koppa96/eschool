using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.Features.Grading.Grades;
using ESchool.Libs.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers.SubjectManagement
{
    [Route("api/school-years/{schoolYearId}/classes/{classId}/subjects/{subjectId}/grades")]
    [ApiController]
    public class ClassSchoolYearSubjectGradesController : ControllerBase
    {
        private readonly IMediator mediator;

        public ClassSchoolYearSubjectGradesController(IMediator mediator)
        {
            this.mediator = mediator;
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