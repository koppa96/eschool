using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Domain;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.Homeworks.Authorization
{
    public class HomeworkDeleteAuthorizationHandler : IRequestAuthorizationHandler<HomeworkDeleteCommand>
    {
        private readonly HomeAssignmentsContext context;
        private readonly IIdentityService identityService;

        public HomeworkDeleteAuthorizationHandler(HomeAssignmentsContext context,
            IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(HomeworkDeleteCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var isAuthorized = await context.Homeworks.Where(x => x.Id == request.Id)
                .Select(x => x.TeacherHomeworks.Any(th => th.Teacher.UserId == currentUserId))
                .SingleAsync(cancellationToken);

            return isAuthorized
                ? RequestAuthorizationResult.Success
                : RequestAuthorizationResult.Failure("Csak javító tanárok törölhetik ki az adott házi feladatot.");
        }
    }
}