using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Common;
using ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Files;
using ESchool.HomeAssignments.Interface.Features.HomeworkSolutions;
using ESchool.HomeAssignments.Interface.Features.HomeworkSolutions.Files;
using ESchool.Libs.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace ESchool.HomeAssignments.Api.Controllers
{
    [Route("api/homeworks/{homeworkId}/solutions/{solutionId}/files")]
    [Route("api/solutions/{solutionId}/files")]
    [Route("api/files")]
    public class FilesController : ESchoolControllerBase
    {
        private readonly IMediator mediator;

        public FilesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(PolicyNames.TeacherOrStudent)]
        [HttpGet("{fileId}")]
        public async Task<FileResult> GetFile(Guid fileId, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new FileGetQuery
            {
                FileId = fileId
            }, cancellationToken);

            var provider = new FileExtensionContentTypeProvider();
            var contentType = "text/plain";
            if (provider.TryGetContentType(result.Name, out var contentTypeResult))
            {
                contentType = contentTypeResult;
            }

            return File(result.Stream, contentType, result.Name);
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