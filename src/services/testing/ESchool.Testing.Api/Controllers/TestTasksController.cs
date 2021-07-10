using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Interface.Commands;
using ESchool.Testing.Interface.Features.TestTasks;
using ESchool.Testing.Interface.Features.TestTasks.CreateEdit;
using ESchool.Testing.Interface.Features.TestTasks.Details;
using ESchool.Testing.Interface.Features.TestTasks.Editor;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Testing.Api.Controllers
{
    [Route("api/tests/{testId}/tasks/{taskId}")]
    [Route("api/tasks/{taskId}")]
    [ApiController]
    public class TestTasksController : ControllerBase
    {
        private readonly IMediator mediator;

        public TestTasksController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        [HttpGet]
        public Task<TestTaskDetailsResponse> GetTestTask(Guid testId, Guid taskId, CancellationToken cancellationToken)
        {
            return mediator.Send(new TestTaskGetQuery
            {
                Id = taskId
            }, cancellationToken);
        }

        [HttpGet("edit-view")]
        public Task<TestTaskEditorResponse> GetTestTaskEditor(Guid testId, Guid taskId,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new TestTaskEditorGetQuery
            {
                Id = taskId
            }, cancellationToken);
        }

        [HttpPut]
        public Task<TestTaskEditorResponse> EditTestTask(Guid testId, Guid taskId,
            [FromBody] TestTaskCreateEditCommand command, CancellationToken cancellationToken)
        {
            return mediator.Send(new EditCommand<TestTaskCreateEditCommand, TestTaskEditorResponse>
            {
                Id = taskId,
                InnerCommand = command
            }, cancellationToken);
        }

        [HttpDelete]
        public Task DeleteTestTask(Guid testId, Guid taskId, CancellationToken cancellationToken)
        {
            return mediator.Send(new TestTaskDeleteCommand
            {
                Id = taskId
            }, cancellationToken);
        }
    }
}