using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Application.Features.Homeworks;
using ESchool.HomeAssignments.Interface.Features.Homeworks;
using ESchool.HomeAssignments.Interface.Features.UserHomeworks;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Interface.Query;
using ESchool.Libs.Interface.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.HomeAssignments.Api.Controllers
{
    [Authorize(PolicyNames.Teacher)]
    [Route("api/lessons/{lessonId}/homeworks")]
    public class LessonHomeworksController : ESchoolControllerBase
    {
        private readonly IMediator mediator;

        public LessonHomeworksController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<PagedListResponse<TeacherHomeworkListResponse>> GetHomeworksForLesson(Guid lessonId,
            [FromQuery] PagedListQuery query, CancellationToken cancellationToken)
        {
            return mediator.Send(new LessonHomeworkListQuery
            {
                LessonId = lessonId,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize
            }, cancellationToken);
        }
    }
}