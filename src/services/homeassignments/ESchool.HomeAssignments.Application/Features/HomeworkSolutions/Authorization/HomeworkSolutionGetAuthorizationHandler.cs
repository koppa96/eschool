using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Domain;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Authorization
{
    public class HomeworkSolutionGetAuthorizationHandler : IRequestAuthorizationHandler<HomeworkSolutionGetQuery>
    {
        private readonly HomeAssignmentsContext context;
        private readonly IIdentityService identityService;

        public HomeworkSolutionGetAuthorizationHandler(HomeAssignmentsContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }

        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(HomeworkSolutionGetQuery request,
            CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var isAuthorized = await context.HomeworkSolutions
                .AnyAsync(x => x.Id == request.Id &&
                               (x.Student.UserId == currentUserId ||
                                x.Homework.Lesson.ClassSchoolYearSubject.ClassSchoolYearSubjectTeachers
                                    .Any(t => t.Teacher.UserId == currentUserId)), cancellationToken);

            return isAuthorized
                ? RequestAuthorizationResult.Success
                : RequestAuthorizationResult.Failure(
                    "A házifeladat megoldást csak a készítője, és a házi feladat javító tanárok tekinthetik meg.");
        }
    }
}