using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Application.Extensions;
using ESchool.HomeAssignments.Domain;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Domain.Services;

namespace ESchool.HomeAssignments.Application.Features.UserHomeworks.Authorization
{
    public class TeacherHomeworkListAuthorizationHandler : IRequestAuthorizationHandler<TeacherHomeworkListQuery>
    {
        private readonly HomeAssignmentsContext context;
        private readonly IIdentityService identityService;

        public TeacherHomeworkListAuthorizationHandler(HomeAssignmentsContext context,
            IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(TeacherHomeworkListQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var isTeacher = await context.Teachers.IsTeacherAtHomework(currentUserId, request.HomeworkId);

            return isTeacher
                ? RequestAuthorizationResult.Success
                : RequestAuthorizationResult.Failure(
                    "Csak olyan tanár frissítheti a résztvevők listáját, aki már fel van véve javító tanárként a feladathoz.");
        }
    }
}