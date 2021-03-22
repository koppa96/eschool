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
        
        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(HomeworkSolutionGetQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var studentHomework = await context.StudentHomeworks.Include(x => x.Homework)
                    .ThenInclude(x => x.TeacherHomeworks)
                        .ThenInclude(x => x.Teacher)
                .Include(x => x.Student)
                .SingleAsync(x => x.StudentId == request.StudentId && x.HomeworkId == request.HomeworkId,
                    cancellationToken);

            if (studentHomework.Student.UserId == currentUserId ||
                studentHomework.Homework.TeacherHomeworks.Any(x => x.Teacher.UserId == currentUserId))
            {
                return RequestAuthorizationResult.Success;
            }
            
            return RequestAuthorizationResult.Failure("A házifeladat megoldást csak a készítője, és a házi feladat javító tanárok tekinthetik meg.");
        }
    }
}