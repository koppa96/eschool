using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons;
using ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons.Common;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers
{
    [Authorize(nameof(TenantRoleType.Administrator))]
    [ApiController]
    [Route("api/school-years/{schoolYearId}/classes/{classId}/subjects/{subjectId}/lessons")]
    public class LessonsController : ControllerBase
    {
        private readonly IMediator mediator;

        public LessonsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<List<LessonListResponse>> GetLessonsBetween(
            Guid schoolYearId,
            Guid classId,
            [FromQuery] DateTime from, 
            [FromQuery] DateTime to,
            [FromQuery] bool showCanceled,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new LessonListQuery
            {
                From = from,
                To = to,
                SchoolYearId = schoolYearId,
                ClassId = classId,
                ShowCanceled = showCanceled
            }, cancellationToken);
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

        [HttpPut("{lessonId}")]
        public Task<LessonDetailsResponse> EditLesson(Guid lessonId, [FromBody] LessonEditCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new EditCommand<LessonEditCommand, LessonDetailsResponse>
            {
                Id = lessonId,
                InnerCommand = command
            }, cancellationToken);
        }

        [HttpDelete("{lessonId}")]
        public Task DeleteLesson(Guid lessonId, CancellationToken cancellationToken)
        {
            return mediator.Send(new LessonDeleteCommand
            {
                Id = lessonId
            }, cancellationToken);
        }
    }
}