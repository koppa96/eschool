using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Application.Cqrs.Query;
using ESchool.Libs.Application.Cqrs.Response;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Domain.Enums;
using ESchool.Testing.Application.Features.TaskAnswers.Common;
using ESchool.Testing.Application.Features.TestAnswers;
using ESchool.Testing.Application.Features.TestAnswers.Common;
using ESchool.Testing.Application.Features.Tests;
using ESchool.Testing.Application.Features.Tests.Common;
using ESchool.Testing.Application.Features.TestTasks;
using ESchool.Testing.Application.Features.TestTasks.Common.Editor;
using ESchool.Testing.Application.Features.TestTasks.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Testing.Api.Controllers
{
    [Route("api/tests")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly IMediator mediator;

        public TestsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        [HttpGet("{testId}/tasks")]
        [Authorize(PolicyNames.TeacherOrStudent)]
        public Task<List<TestTaskListResponse>> ListTasks(Guid testId, CancellationToken cancellationToken)
        {
            return mediator.Send(new TestTaskListQuery
            {
                TestId = testId
            }, cancellationToken);
        }

        [HttpPost("{testId}/tasks")]
        [Authorize(PolicyNames.Teacher)]
        public Task<TestTaskEditorResponse> CreateTask(Guid testId, [FromBody] TestTaskCreateEditCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [HttpGet("{testId}/answers")]
        [Authorize(PolicyNames.Teacher)]
        public Task<PagedListResponse<TestAnswerListResponse>> ListAnswers(Guid testId,
            [FromQuery] PagedListQuery query, CancellationToken cancellationToken)
        {
            return mediator.Send(query.ToTypedQuery<TestAnswerListQuery>(query => query.TestId = testId), cancellationToken);
        }

        [HttpPost("{testId}/answers")]
        [Authorize(PolicyNames.Student)]
        public Task<TestAnswerDetailsResponse> CreateAnswer(Guid testId, CancellationToken cancellationToken)
        {
            return mediator.Send(new TestAnswerCreateCommand
            {
                TestId = testId
            }, cancellationToken);
        }

        [HttpGet("{testId}")]
        [Authorize(PolicyNames.TeacherOrStudent)]
        public Task<TestDetailsResponse> GetTest(Guid testId, CancellationToken cancellationToken)
        {
            return mediator.Send(new TestGetQuery
            {
                TestId = testId
            }, cancellationToken);
        }

        [HttpPost]
        [Authorize(PolicyNames.Teacher)]
        public Task<TestDetailsResponse> CreateTest([FromBody] TestCreateCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [HttpPut("{testId}")]
        [Authorize(PolicyNames.Teacher)]
        public Task<TestDetailsResponse> EditTest(Guid testId, [FromBody] TestEditCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(new EditCommand<TestEditCommand, TestDetailsResponse>
            {
                Id = testId,
                InnerCommand = command
            }, cancellationToken);
        }

        [HttpPatch("{testId}/start")]
        [Authorize(PolicyNames.Teacher)]
        public Task<TestDetailsResponse> StartTest(Guid testId, CancellationToken cancellationToken)
        {
            return mediator.Send(new TestStartCommand
            {
                Id = testId
            }, cancellationToken);
        }

        [HttpPatch("{testId}/close")]
        [Authorize(PolicyNames.Teacher)]
        public Task<TestDetailsResponse> CloseTest(Guid testId, CancellationToken cancellationToken)
        {
            return mediator.Send(new TestCloseCommand
            {
                Id = testId
            }, cancellationToken);
        }

        [HttpPatch("{testId}/correct")]
        [Authorize(PolicyNames.Teacher)]
        public Task CorrectTest(Guid testId, CancellationToken cancellationToken)
        {
            return mediator.Send(new TestCorrectCommand
            {
                TestId = testId
            }, cancellationToken);
        }

        [HttpDelete("{testId}")]
        [Authorize(PolicyNames.Teacher)]
        public Task DeleteTest(Guid testId, CancellationToken cancellationToken)
        {
            return mediator.Send(new TestDeleteCommand
            {
                Id = testId
            }, cancellationToken);
        }
    }
}