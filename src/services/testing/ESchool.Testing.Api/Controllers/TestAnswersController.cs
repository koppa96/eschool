using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.AspNetCore;
using ESchool.Testing.Application.Features.TestAnswers;
using ESchool.Testing.Application.Features.TestAnswers.Common;
using ESchool.Testing.Interface.Features.TestAnswers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Testing.Api.Controllers
{
    [Route("api/tests/{testId}/answers/{answerId}")]
    [Route("api/test-answers/{answerId}")]
    public class TestAnswersController : ESchoolControllerBase
    {
        private readonly IMediator mediator;

        public TestAnswersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<TestAnswerDetailsResponse> GetTestAnswers(Guid testId, Guid answerId, CancellationToken cancellationToken)
        {
            return mediator.Send(new TestAnswerGetQuery
            {
                Id = answerId
            }, cancellationToken);
        }

        [HttpPatch("close")]
        public Task CloseTestAnswer(Guid testId, Guid answerId, CancellationToken cancellationToken)
        {
            return mediator.Send(new TestAnswerCloseCommand
            {
                Id = answerId
            }, cancellationToken);
        }
    }
}