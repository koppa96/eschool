using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Application.Features.Homeworks;
using ESchool.HomeAssignments.Application.Features.Homeworks.Common;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.AspNetCore;
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
        [HttpGet("{id}")]
        public Task<HomeworkDetailsResponse> GetHomework(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new HomeworkGetQuery
            {
                Id = id
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
        [HttpPut("{id}")]
        public Task<HomeworkDetailsResponse> EditHomework(Guid id, [FromBody] HomeworkEditCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new EditCommand<HomeworkEditCommand, HomeworkDetailsResponse>
            {
                Id = id,
                InnerCommand = command
            }, cancellationToken);
        }

        [Authorize(PolicyNames.Teacher)]
        [HttpDelete("{id}")]
        public Task DeleteHomework(Guid id, CancellationToken cancellationToken)
        {
            return mediator.Send(new HomeworkDeleteCommand
            {
                Id = id
            }, cancellationToken);
        }
    }
}