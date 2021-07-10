using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.Users.Teachers;
using ESchool.ClassRegister.Interface.Features.Users.Teachers;
using ESchool.Libs.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers.SubjectManagement
{
    [Route("api/school-years/{schoolYearId}/classes/{classId}/subjects/{subjectId}/teachers")]
    [Authorize(PolicyNames.Administrator)]
    [ApiController]
    public class ClassSchoolYearSubjectTeachersController : ControllerBase
    {
        private readonly IMediator mediator;

        public ClassSchoolYearSubjectTeachersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("{teacherId}")]
        public Task AssignTeacher(
            Guid schoolYearId,
            Guid classId,
            Guid subjectId,
            Guid teacherId,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new AssignTeacherToClassSchoolYearSubjectCommand
            {
                SchoolYearId = schoolYearId,
                ClassId = classId,
                SubjectId = subjectId,
                TeacherId = teacherId
            }, cancellationToken);
        }

        [HttpDelete("{teacherId}")]
        public Task RemoveTeacher(
            Guid schoolYearId,
            Guid classId,
            Guid subjectId,
            Guid teacherId,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new RemoveTeacherFromClassSchoolYearSubjectCommand
            {
                SchoolYearId = schoolYearId,
                ClassId = classId,
                SubjectId = subjectId,
                TeacherId = teacherId
            }, cancellationToken);
        }
    }
}