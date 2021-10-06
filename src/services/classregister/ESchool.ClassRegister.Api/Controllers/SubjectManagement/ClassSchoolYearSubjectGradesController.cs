using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.Features.Grading.Grades;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Interface.Query;
using ESchool.Libs.Interface.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers.SubjectManagement
{
    [Route("api/school-years/{schoolYearId}/classes/{classId}/subjects/{subjectId}/grades")]
    public class ClassSchoolYearSubjectGradesController : ESchoolControllerBase
    {
        private readonly IMediator mediator;

        public ClassSchoolYearSubjectGradesController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        [Authorize(PolicyNames.TeacherOrAdministrator)]
        [HttpGet]
        public Task<PagedListResponse<GradeListByClassSchoolYearSubjectResponse>> ListGradesBySubject(
            Guid schoolYearId,
            Guid classId,
            Guid subjectId,
            [FromQuery] PagedListQuery query,
            CancellationToken cancellationToken)
        {
            return mediator.Send(query.ToTypedQuery<GradeListByClassSchoolYearSubjectQuery>(x =>
            {
                x.SchoolYearId = schoolYearId;
                x.ClassId = classId;
                x.SubjectId = subjectId;
            }), cancellationToken);
        }
    }
}