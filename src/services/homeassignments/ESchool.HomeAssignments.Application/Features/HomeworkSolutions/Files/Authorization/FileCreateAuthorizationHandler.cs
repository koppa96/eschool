using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Domain;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Files.Authorization
{
    public class FileCreateAuthorizationHandler : IRequestAuthorizationHandler<FileCreateCommand>
    {
        private readonly HomeAssignmentsContext context;
        private readonly IIdentityService identityService;

        public FileCreateAuthorizationHandler(HomeAssignmentsContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }

        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(FileCreateCommand request,
            CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var isAuthorized = await context.HomeworkSolutions.AnyAsync(
                x => x.Id == request.SolutionId && x.Student.UserId == currentUserId, cancellationToken);

            return isAuthorized
                ? RequestAuthorizationResult.Success
                : RequestAuthorizationResult.Failure("A felatmegoldást csak az azt létrehozó diák módosíthatja.");
        }
    }
}