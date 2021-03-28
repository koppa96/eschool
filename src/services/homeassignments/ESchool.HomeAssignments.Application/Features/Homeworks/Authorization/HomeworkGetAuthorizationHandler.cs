using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Domain;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.Homeworks.Authorization
{
    public class HomeworkGetAuthorizationHandler : IRequestAuthorizationHandler<HomeworkGetQuery>
    {
        private readonly HomeAssignmentsContext context;
        private readonly IIdentityService identityService;

        public HomeworkGetAuthorizationHandler(HomeAssignmentsContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }

        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(HomeworkGetQuery request,
            CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var isAuthorized = await context.Homeworks.AnyAsync(x =>
                x.Id == request.Id &&
                (x.Lesson.ClassSchoolYearSubject.ClassSchoolYearSubjectStudents.Any(s =>
                     s.Student.UserId == currentUserId) ||
                 x.Lesson.ClassSchoolYearSubject.ClassSchoolYearSubjectTeachers.Any(t =>
                     t.Teacher.UserId == currentUserId)), cancellationToken);

            return isAuthorized
                ? RequestAuthorizationResult.Success
                : RequestAuthorizationResult.Failure(
                    "A házi feladatot csak a javító tanárok és a megoldó diákok tekinthetik meg.");
        }
    }
}