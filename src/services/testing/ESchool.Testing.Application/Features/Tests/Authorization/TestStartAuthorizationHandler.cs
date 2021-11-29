using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Domain.Services;
using ESchool.Testing.Domain;
using ESchool.Testing.Interface.Features.Tests;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Testing.Application.Features.Tests.Authorization
{
    public class TestStartAuthorizationHandler : IRequestAuthorizationHandler<TestStartCommand>
    {
        private readonly TestingContext context;
        private readonly IIdentityService identityService;

        public TestStartAuthorizationHandler(TestingContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }

        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(TestStartCommand request,
            CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var isAuthorized = await context.Tests.AnyAsync(
                x => x.Id == request.Id &&
                     x.ClassSchoolYearSubject.ClassSchoolYearSubjectTeachers
                         .Any(t => t.Teacher.UserId == currentUserId),
                cancellationToken);

            return isAuthorized
                ? RequestAuthorizationResult.Success
                : RequestAuthorizationResult.Failure(
                    "Csak olyan tanár indíthatja el a dolgozatot, aki tanítja az adott tárgyat.");
        }
    }
}