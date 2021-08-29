using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Interface.Features.HomeworkSolutions;
using ESchool.Libs.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.HomeAssignments.Api.Controllers
{
    [Route("api/homeworks/{homeworkId}/solutions")]
    [ApiController]
    public class HomeworkSolutionsController : ControllerBase
    {
        private readonly IMediator mediator;

        public HomeworkSolutionsController(IMediator mediator)
        {
            this.mediator = mediator;
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
    }
}