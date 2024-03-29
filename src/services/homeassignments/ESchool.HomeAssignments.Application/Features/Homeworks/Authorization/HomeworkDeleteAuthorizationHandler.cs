﻿using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Application.Extensions;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Interface.Features.Homeworks;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Domain.Services;

namespace ESchool.HomeAssignments.Application.Features.Homeworks.Authorization
{
    public class HomeworkDeleteAuthorizationHandler : IRequestAuthorizationHandler<HomeworkDeleteCommand>
    {
        private readonly HomeAssignmentsContext context;
        private readonly IIdentityService identityService;

        public HomeworkDeleteAuthorizationHandler(HomeAssignmentsContext context,
            IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(HomeworkDeleteCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var isTeacher = await context.Homeworks.IsTeacherAtHomework(currentUserId, request.Id, cancellationToken);

            return isTeacher
                ? RequestAuthorizationResult.Success
                : RequestAuthorizationResult.Failure("Csak javító tanárok törölhetik ki az adott házi feladatot.");
        }
    }
}