using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Interface.Features.Homeworks;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Interface.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.HomeAssignments.Api.Controllers
{
    [Route("api/homeworks")]
    [ApiController]
    public class HomeworksController : ControllerBase
    {
        private readonly IMediator mediator;

        public HomeworksController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(PolicyNames.TeacherOrStudent)]
        [HttpGet("{homeworkId}")]
        public Task<HomeworkDetailsResponse> GetHomework(Guid homeworkId, CancellationToken cancellationToken)
        {
            return mediator.Send(new HomeworkGetQuery
            {
                Id = homeworkId
            }, cancellationToken);
        }

        [Authorize(PolicyNames.Teacher)]
        [HttpPost]
        public Task<HomeworkDetailsResponse> CreateHomework([FromBody] HomeworkCreateCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [Authorize(PolicyNames.Teacher)]
        [HttpPut("{homeworkId}")]
        public Task<HomeworkDetailsResponse> EditHomework(Guid homeworkId, [FromBody] HomeworkEditCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new EditCommand<HomeworkEditCommand, HomeworkDetailsResponse>
            {
                Id = homeworkId,
                InnerCommand = command
            }, cancellationToken);
        }

        [Authorize(PolicyNames.Teacher)]
        [HttpDelete("{homeworkId}")]
        public Task DeleteHomework(Guid homeworkId, CancellationToken cancellationToken)
        {
            return mediator.Send(new HomeworkDeleteCommand
            {
                Id = homeworkId
            }, cancellationToken);
        }
    }
}