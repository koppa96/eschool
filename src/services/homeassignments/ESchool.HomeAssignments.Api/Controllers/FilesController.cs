using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Common;
using ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Files;
using ESchool.HomeAssignments.Interface.Features.HomeworkSolutions;
using ESchool.Libs.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.HomeAssignments.Api.Controllers
{
    [Route("api/homeworks/{homeworkId}/solutions/{solutionId}/files")]
    [Route("api/solutions/{solutionId}/files")]
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IMediator mediator;

        public FilesController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        [Authorize(PolicyNames.Student)]
        [HttpDelete("{fileId}")]
        public Task<HomeworkSolutionResponse> DeleteFile(Guid fileId, CancellationToken cancellationToken)
        {
            return mediator.Send(new FileDeleteCommand
            {
                Id = fileId
            }, cancellationToken);
        }
    }
}