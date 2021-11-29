using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Services;
using ESchool.HomeAssignments.Interface.Features.HomeworkSolutions.Files;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Files
{
    public class FileGetHandler : IRequestHandler<FileGetQuery, FileGetResponse>
    {
        private readonly ISolutionFileHandlerService solutionFileHandlerService;
        private readonly HomeAssignmentsContext context;
        private readonly IIdentityService identityService;

        public FileGetHandler(
            ISolutionFileHandlerService solutionFileHandlerService,
            HomeAssignmentsContext context,
            IIdentityService identityService)
        {
            this.solutionFileHandlerService = solutionFileHandlerService;
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<FileGetResponse> Handle(FileGetQuery request, CancellationToken cancellationToken)
        {
            var file = await context.Files.SingleAsync(x => x.Id == request.FileId, cancellationToken);
            return new FileGetResponse
            {
                Name = file.FileName,
                Stream = await solutionFileHandlerService.OpenAsync(file.HomeWorkSolutionId, file.FileName)
            };
        }
    }
}