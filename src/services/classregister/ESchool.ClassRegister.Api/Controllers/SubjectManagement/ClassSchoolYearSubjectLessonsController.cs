using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.SubjectManagement.Absences;
using ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.Absences;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.Lessons;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Interface.Commands;
using ESchool.Libs.Interface.Query;
using ESchool.Libs.Interface.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers.SubjectManagement
{
    [Route("api/lessons")]
    [Route("api/school-years/{schoolYearId}/classes/{classId}/subjects/{subjectId}/lessons")]
    public class ClassSchoolYearSubjectLessonsController : ESchoolControllerBase
    {
        private readonly IMediator mediator;

        public ClassSchoolYearSubjectLessonsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Authorize(PolicyNames.AnyRole)]
        public Task<PagedListResponse<LessonListResponse>> ListLessons(
            Guid schoolYearId,
            Guid classId,
            Guid subjectId,
            [FromQuery] PagedListQuery query,
            CancellationToken cancellationToken)
        {
            return mediator.Send(query.ToTypedQuery<AdminLessonListQuery>(x =>
            {
                x.SchoolYearId = schoolYearId;
                x.ClassId = classId;
                x.SubjectId = subjectId;
            }), cancellationToken);
        }

        [HttpGet("{lessonId}")]
        [Authorize(PolicyNames.AnyRole)]
        public Task<LessonDetailsResponse> GetLesson(Guid lessonId, CancellationToken cancellationToken)
        {
            return mediator.Send(new LessonGetQuery { Id = lessonId }, cancellationToken);
        }
        
        [HttpPost]
        [Authorize(PolicyNames.Administrator)]
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
        [Authorize(PolicyNames.TeacherOrAdministrator)]
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