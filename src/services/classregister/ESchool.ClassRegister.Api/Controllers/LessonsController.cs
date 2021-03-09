using System;
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
    [ApiController]
    [Route("api/lessons")]
    public class LessonsController : ControllerBase
    {
        private readonly IMediator mediator;

        public LessonsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPut("{lessonId}")]
        [Authorize(nameof(TenantRoleType.Administrator))]
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
        [Authorize(nameof(TenantRoleType.Teacher))]
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
        public Task DeleteLesson(Guid lessonId, CancellationToken cancellationToken)
        {
            return mediator.Send(new LessonDeleteCommand
            {
                Id = lessonId
            }, cancellationToken);
        }
    }
}