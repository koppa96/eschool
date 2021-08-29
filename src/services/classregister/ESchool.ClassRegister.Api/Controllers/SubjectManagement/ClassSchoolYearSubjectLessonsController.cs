using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.SubjectManagement.Absences;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.Lessons;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Interface.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers.SubjectManagement
{
    [ApiController]
    [Route("api/lessons")]
    [Route("api/school-years/{schoolYearId}/classes/{classId}/subjects/{subjectId}/lessons")]
    public class ClassSchoolYearSubjectLessonsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ClassSchoolYearSubjectLessonsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        [HttpPost]
        public Task<LessonDetailsResponse> CreateLesson(
            Guid schoolYearId,
            Guid classId,
            Guid subjectId,
            [FromBody] LessonCreateCommand.LessonCreateCommandBody body,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new LessonCreateCommand
            {
                ClassId = classId,
                SchoolYearId = schoolYearId,
                SubjectId = subjectId,
                Body = body
            }, cancellationToken);
        }

        [HttpPost("{lessonId}/absences/{studentId}")]
        [Authorize(PolicyNames.Teacher)]
        public Task CreateAbsence(Guid lessonId, Guid studentId, CancellationToken cancellationToken)
        {
            return mediator.Send(new AbsenceCreateCommand
            {
                LessonId = lessonId,
                StudentId = studentId
            }, cancellationToken);
        }

        [HttpPut("{lessonId}")]
        [Authorize(PolicyNames.Administrator)]
        public Task<LessonDetailsResponse> EditLesson(Guid lessonId, [FromBody] LessonEditCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new EditCommand<LessonEditCommand, LessonDetailsResponse>
            {
                Id = lessonId,
                InnerCommand = command
            }, cancellationToken);
        }

        [HttpPatch("{lessonId}")]
        [Authorize(PolicyNames.TeacherOrAdministrator)]
        public Task<LessonDetailsResponse> SetLessonCancellation(Guid lessonId,
            [FromBody] LessonCancellationSetCommand command, CancellationToken cancellationToken)
        {
            return mediator.Send(new EditCommand<LessonCancellationSetCommand, LessonDetailsResponse>
            {
                Id = lessonId,
                InnerCommand = command
            }, cancellationToken);
        }
        
        [HttpDelete("{lessonId}")]
        [Authorize(PolicyNames.Administrator)]
        public Task DeleteLesson(Guid lessonId, CancellationToken cancellationToken)
        {
            return mediator.Send(new LessonDeleteCommand
            {
                Id = lessonId
            }, cancellationToken);
        }
    }
}