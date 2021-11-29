using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Domain.Services;
using ESchool.Libs.Interface.Commands;
using ESchool.Testing.Domain;
using ESchool.Testing.Interface.Features.Tests;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Testing.Application.Features.Tests.Authorization
{
    public class TestCreateEditStartCloseAuthorizationHandler : IRequestAuthorizationHandler<TestCreateCommand>,
        IRequestAuthorizationHandler<EditCommand<TestEditCommand, TestDetailsResponse>>,
        IRequestAuthorizationHandler<TestStartCommand>,
        IRequestAuthorizationHandler<TestCloseCommand>
    {
        private readonly IIdentityService identityService;
        private readonly TestingContext context;

        public TestCreateEditStartCloseAuthorizationHandler(IIdentityService identityService,
            TestingContext context)
        {
            this.identityService = identityService;
            this.context = context;
        }
        
        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(TestCreateCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var isAuthorized = await context.ClassSchoolYearSubjectTeachers.AnyAsync(
                x => x.ClassSchoolYearSubjectId == request.ClassSchoolYearSubjectId &&
                     x.Teacher.UserId == currentUserId, cancellationToken);

            return isAuthorized
                ? RequestAuthorizationResult.Success
                : RequestAuthorizationResult.Failure("Csak a tárgyat tanító tanárok hozhatnak létre dolgozatot.");
        }

        private async Task<RequestAuthorizationResult> CanModifyTestAsync(Guid testId,
            CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var isAuthorized = await context.ClassSchoolYearSubjectTeachers.AnyAsync(
                x => x.Id == testId &&
                     x.Teacher.UserId == currentUserId, cancellationToken);

            return isAuthorized
                ? RequestAuthorizationResult.Success
                : RequestAuthorizationResult.Failure("Csak a tárgyat tanító tanárok módosíthatják a dolgozatot.");
        }

        public Task<RequestAuthorizationResult> IsAuthorizedAsync(EditCommand<TestEditCommand, TestDetailsResponse> request, CancellationToken cancellationToken)
        {
            return CanModifyTestAsync(request.Id, cancellationToken);
        }

        public Task<RequestAuthorizationResult> IsAuthorizedAsync(TestStartCommand request, CancellationToken cancellationToken)
        {
            return CanModifyTestAsync(request.Id, cancellationToken);
        }

        public Task<RequestAuthorizationResult> IsAuthorizedAsync(TestCloseCommand request, CancellationToken cancellationToken)
        {
            return CanModifyTestAsync(request.Id, cancellationToken);
        }
    }
}