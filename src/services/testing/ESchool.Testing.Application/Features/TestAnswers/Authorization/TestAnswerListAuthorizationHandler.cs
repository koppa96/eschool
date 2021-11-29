using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Domain.Services;
using ESchool.Testing.Domain;
using ESchool.Testing.Interface.Features.TestAnswers;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Testing.Application.Features.TestAnswers.Authorization
{
    public class TestAnswerListAuthorizationHandler : IRequestAuthorizationHandler<TestAnswerListQuery>
    {
        private readonly TestingContext context;
        private readonly IIdentityService identityService;

        public TestAnswerListAuthorizationHandler(TestingContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(TestAnswerListQuery request, CancellationToken cancellationToken)
        {
            var test = await context.Tests.Include(x => x.ClassSchoolYearSubject)
                    .ThenInclude(x => x.ClassSchoolYearSubjectTeachers)
                        .ThenInclude(x => x.Teacher)
                .SingleAsync(x => x.Id == request.TestId, cancellationToken);

            var currentUserId = identityService.GetCurrentUserId();
            var isAuthorized = test.ClassSchoolYearSubject.ClassSchoolYearSubjectTeachers.Any(x => x.Teacher.UserId == currentUserId);

            return isAuthorized
                ? RequestAuthorizationResult.Success
                : RequestAuthorizationResult.Failure("Csak a javító tanárok listázhatják a dolgozatra beküldött megoldásokat.");
        }
    }
}