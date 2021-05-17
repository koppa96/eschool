using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;
using ESchool.Testing.Domain;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Testing.Application.Features.TaskAnswers.Authorization
{
    public class TaskAnswerGetAuthorizationHandler : IRequestAuthorizationHandler<TaskAnswerGetQuery>
    {
        private readonly TestingContext context;
        private readonly IIdentityService identityService;

        public TaskAnswerGetAuthorizationHandler(TestingContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(TaskAnswerGetQuery request, CancellationToken cancellationToken)
        {
            var isAuthorized = false;
            var currentUserId = identityService.GetCurrentUserId();
            if (identityService.IsInRole(TenantRoleType.Student))
            {
                isAuthorized = await context.TaskAnswers.AnyAsync(
                    x => x.Id == request.Id && x.TestAnswer.StudentTest.Student.UserId == currentUserId,
                    cancellationToken);
            }
            else if (identityService.IsInRole(TenantRoleType.Teacher))
            {
                isAuthorized = await context.TaskAnswers.AnyAsync(
                    x => x.Id == request.Id &&
                         x.TestAnswer.StudentTest.Test.ClassSchoolYearSubject.ClassSchoolYearSubjectTeachers.Any(t =>
                             t.Teacher.UserId == currentUserId), cancellationToken);
            }

            return isAuthorized
                ? RequestAuthorizationResult.Success
                : RequestAuthorizationResult.Failure(
                    "A feladatmegoldást csak a beadó diák és a javító tanár tekintheti meg.");
        }
    }
}