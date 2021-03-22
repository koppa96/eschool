using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Domain;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.Homeworks.Authorization
{
    public class HomeworkStudentAndTeacherListRefreshAuthorizationHandler : 
        IRequestAuthorizationHandler<HomeworkStudentAndTeacherListRefreshCommand>
    {
        private readonly HomeAssignmentsContext context;
        private readonly IIdentityService identityService;

        public HomeworkStudentAndTeacherListRefreshAuthorizationHandler(HomeAssignmentsContext context,
            IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(HomeworkStudentAndTeacherListRefreshCommand request,
            CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var isTeacher = await context.Teachers.AnyAsync(
                x => x.TeacherHomeworks.Any(th => th.HomeworkId == request.HomeworkId) && x.UserId == currentUserId,
                cancellationToken);

            return isTeacher
                ? RequestAuthorizationResult.Success
                : RequestAuthorizationResult.Failure(
                    "Csak olyan tanár frissítheti a résztvevők listáját, aki már fel van véve javító tanárként a feladathoz.");
        }
    }
}