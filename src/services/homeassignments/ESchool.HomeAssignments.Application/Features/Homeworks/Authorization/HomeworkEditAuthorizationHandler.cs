using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Application.Extensions;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Interface.Features.Homeworks;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Domain.Services;
using ESchool.Libs.Interface.Commands;

namespace ESchool.HomeAssignments.Application.Features.Homeworks.Authorization
{
    public class HomeworkEditAuthorizationHandler : IRequestAuthorizationHandler<EditCommand<HomeworkEditCommand, HomeworkDetailsResponse>>
    {
        private readonly HomeAssignmentsContext context;
        private readonly IIdentityService identityService;

        public HomeworkEditAuthorizationHandler(HomeAssignmentsContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(EditCommand<HomeworkEditCommand, HomeworkDetailsResponse> request,
            CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var isTeacher = await context.Homeworks.IsTeacherAtHomework(currentUserId, request.Id);

            return isTeacher
                ? RequestAuthorizationResult.Success
                : RequestAuthorizationResult.Failure("Csak javító tanárok módosíthatják az adott házi feladatot.");
        }
    }
}