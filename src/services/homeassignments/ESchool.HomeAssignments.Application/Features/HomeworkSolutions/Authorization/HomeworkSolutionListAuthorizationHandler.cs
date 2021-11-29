using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Application.Extensions;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Interface.Features.HomeworkSolutions;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Domain.Services;

namespace ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Authorization
{
    public class HomeworkSolutionListAuthorizationHandler : IRequestAuthorizationHandler<HomeworkSolutionListQuery>
    {
        private readonly HomeAssignmentsContext context;
        private readonly IIdentityService identityService;

        public HomeworkSolutionListAuthorizationHandler(HomeAssignmentsContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(HomeworkSolutionListQuery request, CancellationToken cancellationToken)
        {
            var isAuthorized = await context.Homeworks.IsTeacherAtHomework(identityService.GetCurrentUserId(),
                request.HomeworkId, cancellationToken);

            return isAuthorized
                ? RequestAuthorizationResult.Success
                : RequestAuthorizationResult.Failure("A feladatra beküldött megoldásokat csak a javító tanárok listázhatják.");
        }
    }
}