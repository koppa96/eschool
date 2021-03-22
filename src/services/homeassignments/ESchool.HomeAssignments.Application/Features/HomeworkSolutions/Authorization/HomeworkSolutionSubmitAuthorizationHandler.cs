using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Domain;
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
            var solution = await context.HomeworkSolutions.Include(x => x.StudentHomework)
                .ThenInclude(x => x.Student)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);

            return solution.StudentHomework.Student.UserId == currentUserId
                ? RequestAuthorizationResult.Success
                : RequestAuthorizationResult.Failure("A házi feladatot csak a készítő diák adhatja be.");
        }
    }
}