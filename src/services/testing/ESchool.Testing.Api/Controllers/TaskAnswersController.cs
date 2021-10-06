using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.AspNetCore;
using ESchool.Testing.Application.Features.TaskAnswers;
using ESchool.Testing.Application.Features.TaskAnswers.Common;
using ESchool.Testing.Application.Features.TaskAnswers.CreateEdit;
using ESchool.Testing.Interface.Features.TaskAnswers.CreateEdit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Testing.Api.Controllers
{
    [Route("api/tests/{testId}/answers/{answerId}/taks-answers")]
    [Route("api/test-answers/{answerId}/task-answers")]
    [Route("api/task-answers")]
    public class TaskAnswersController : ESchoolControllerBase
    {
        private readonly IMediator mediator;

        public TaskAnswersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{taskAnswerId}")]
        public Task<TaskAnswerResponse> GetTaskAnswer(Guid taskAnswerId, CancellationToken cancellationToken)
        {
            return mediator.Send(new TaskAnswerGetQuery
            {
                Id = taskAnswerId
            }, cancellationToken);
        }

        [HttpPut]
        public Task<TaskAnswerResponse> CreateOrEditTaskAnswer([FromBody] TaskAnswerCreateEditCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [HttpPatch("{taskAnswerId}")]
        public Task CorrectTaskAnswer(Guid taskAnswerId, [FromQuery] int givenPoints,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new TaskAnswerCorrectCommand
            {
                TaskAnswerId = taskAnswerId,
                GivenPoints = givenPoints
            }, cancellationToken);
        }

        [HttpDelete("{taskAnswerId}")]
        public Task DeleteTaskAnswer(Guid taskAnswerId, CancellationToken cancellationToken)
        {
            return mediator.Send(new TaskAnswerDeleteCommand
            {
                Id = taskAnswerId
            }, cancellationToken);
        }
        
    }
}