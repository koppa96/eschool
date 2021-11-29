using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Interface.Features.HomeworkSolutions;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Authorization
{
    public class HomeworkSolutionSubmitAuthorizationHandler : IRequestAuthorizationHandler<HomeworkSolutionSubmitCommand>
    {
        private readonly HomeAssignmentsContext context;
        private readonly IIdentityService identityService;

        public HomeworkSolutionSubmitAuthorizationHandler(HomeAssignmentsContext context,
            IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(HomeworkSolutionSubmitCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var isAuthorized =
                await context.HomeworkSolutions.AnyAsync(x => x.Id == request.Id && x.Student.UserId == currentUserId,
                    cancellationToken);

            return isAuthorized
                ? RequestAuthorizationResult.Success
                : RequestAuthorizationResult.Failure("A házi feladatot csak a készítő diák adhatja be.");
        }
    }
}