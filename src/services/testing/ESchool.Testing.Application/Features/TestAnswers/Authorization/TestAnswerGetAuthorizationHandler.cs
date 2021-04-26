using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Domain.Services;
using ESchool.Testing.Domain;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Testing.Application.Features.TestAnswers.Authorization
{
    public class TestAnswerGetAuthorizationHandler : IRequestAuthorizationHandler<TestAnswerGetQuery>
    {
        private readonly TestingContext context;
        private readonly IIdentityService identityService;

        public TestAnswerGetAuthorizationHandler(TestingContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(TestAnswerGetQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var isAuthorized = await context.TestAnswers.AnyAsync(x =>
                x.Id == request.Id &&
                (x.StudentTest.Student.UserId == currentUserId ||
                 x.StudentTest.Test.ClassSchoolYearSubject.ClassSchoolYearSubjectTeachers.Any(t => t.Teacher.UserId == currentUserId)),
                cancellationToken);

            return isAuthorized
                ? RequestAuthorizationResult.Success
                : RequestAuthorizationResult.Failure(
                    "A dolgozatbeadáshoz csak a beadó diák, és a javító tanárok férhetnek hozzá.");
        }
    }
}