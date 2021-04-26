using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Domain.Services;
using ESchool.Testing.Domain;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Testing.Application.Features.TaskAnswers.Authorization
{
    public class TaskAnswerDeleteAuthorizationHandler : IRequestAuthorizationHandler<TaskAnswerDeleteCommand>
    {
        private readonly TestingContext context;
        private readonly IIdentityService identityService;

        public TaskAnswerDeleteAuthorizationHandler(TestingContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(TaskAnswerDeleteCommand request, CancellationToken cancellationToken)
        {
            var answer = await context.TaskAnswers.Include(x => x.TestAnswer)
                    .ThenInclude(x => x.StudentTest)
                        .ThenInclude(x => x.Student)
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            var currentUserId = identityService.GetCurrentUserId();
            if (answer == null || answer.TestAnswer.StudentTest.Student.UserId == currentUserId)
            {
                return RequestAuthorizationResult.Success;
            }
            
            return RequestAuthorizationResult.Failure("A feladatbeadást csak az azt beadó tanuló törölheti.");
        }
    }
}