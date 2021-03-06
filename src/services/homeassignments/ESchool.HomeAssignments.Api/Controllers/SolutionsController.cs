using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Application.Features.HomeworkReviews;
using ESchool.HomeAssignments.Application.Features.HomeworkReviews.Common;
using ESchool.HomeAssignments.Application.Features.HomeworkSolutions;
using ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Common;
using ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Files;
using ESchool.Libs.Application.Cqrs.Query;
using ESchool.Libs.Application.Cqrs.Response;
using ESchool.Libs.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.HomeAssignments.Api.Controllers
{
    [Route("api/homeworks/{homeworkId}/solutions")]
    [Route("api/solutions")]
    [ApiController]
    public class SolutionsController : ControllerBase
    {
        private readonly IMediator mediator;

        public SolutionsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        [Authorize(PolicyNames.Teacher)]
        [HttpGet]
        public Task<PagedListResponse<HomeworkSolutionListResponse>> ListSolutions(Guid homeworkId,
            [FromQuery] int pageIndex, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            return mediator.Send(new HomeworkSolutionListQuery
            {
                HomeworkId = homeworkId,
                PageIndex = pageIndex,
                PageSize = pageSize == 0 ? PagedListQuery.DefaultPageSize : pageSize
            }, cancellationToken);
        }

        [Authorize(PolicyNames.Student)]
        [HttpPost]
        public Task<HomeworkSolutionResponse> CreateSolution(Guid homeworkId, CancellationToken cancellationToken)
        {
            return mediator.Send(new HomeworkSolutionCreateCommand
            {
                HomeworkId = homeworkId
            }, cancellationToken);
        }

        [Authorize(PolicyNames.TeacherOrStudent)]
        [HttpGet("{solutionId}")]
        public Task<HomeworkSolutionResponse> GetSolution(Guid solutionId, CancellationToken cancellationToken)
        {
            return mediator.Send(new HomeworkSolutionGetQuery
            {
                Id = solutionId
            }, cancellationToken);
        }

        [Authorize(PolicyNames.Student)]
        [HttpPatch("{solutionId}")]
        public Task<HomeworkSolutionResponse> SubmitSolution(Guid solutionId, CancellationToken cancellationToken)
        {
            return mediator.Send(new HomeworkSolutionSubmitCommand
            {
                Id = solutionId
            }, cancellationToken);
        }

        [Authorize(PolicyNames.Teacher)]
        [HttpPost("{solutionId}/review")]
        public Task<HomeworkReviewResponse> CreateReview(Guid solutionId,
            [FromBody] HomeworkReviewCreateCommand.Body body, CancellationToken cancellationToken)
        {
            return mediator.Send(new HomeworkReviewCreateCommand
            {
                HomeworkSolutionId = solutionId,
                RequestBody = body
            }, cancellationToken);
        }

        [Authorize(PolicyNames.Student)]
        [HttpPost("{solutionId}/files")]
        public async Task<HomeworkSolutionResponse> UploadFile(Guid solutionId, IFormFile file,
            CancellationToken cancellationToken)
        {
            await using var stream = file.OpenReadStream();
            return await mediator.Send(new FileCreateCommand
            {
                SolutionId = solutionId,
                FileName = file.FileName,
                FileStream = stream
            }, cancellationToken);
        }
    }
}