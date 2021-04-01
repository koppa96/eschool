using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Application.Features.Homeworks;
using ESchool.HomeAssignments.Application.Features.Homeworks.Common;
using ESchool.HomeAssignments.Application.Features.HomeworkSolutions;
using ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Common;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Application.Cqrs.Query;
using ESchool.Libs.Application.Cqrs.Response;
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
        [HttpGet("{homeworkId}")]
        public Task<HomeworkDetailsResponse> GetHomework(Guid homeworkId, CancellationToken cancellationToken)
        {
            return mediator.Send(new HomeworkGetQuery
            {
                Id = homeworkId
            }, cancellationToken);
        }
        
        [Authorize(PolicyNames.Teacher)]
        [HttpGet("{homeworkId}/solutions")]
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
        [HttpPost("{homeworkId/solutions}")]
        public Task<HomeworkSolutionResponse> CreateSolution(Guid homeworkId, CancellationToken cancellationToken)
        {
            return mediator.Send(new HomeworkSolutionCreateCommand
            {
                HomeworkId = homeworkId
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