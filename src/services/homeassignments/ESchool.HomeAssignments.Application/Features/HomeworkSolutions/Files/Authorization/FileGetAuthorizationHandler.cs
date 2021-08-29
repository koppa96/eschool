using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Domain;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.HomeworkSolutions.Files.Authorization
{
    public class FileGetAuthorizationHandler : IRequestAuthorizationHandler<FileGetQuery>
    {
        private readonly HomeAssignmentsContext context;
        private readonly IIdentityService identityService;

        public FileGetAuthorizationHandler(HomeAssignmentsContext context,
            IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(FileGetQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            
            var file = await context.Files.Include(x => x.HomeworkSolution)
                    .ThenInclude(x => x.Student)
                    .Include(x => x.HomeworkSolution)
                    .ThenInclude(x => x.Homework)
                        .ThenInclude(x => x.Lesson)
                            .ThenInclude(x => x.ClassSchoolYearSubject)
                                .ThenInclude(x => x.ClassSchoolYearSubjectTeachers)
                                    .ThenInclude(x => x.Teacher)
                .SingleAsync(x => x.Id == request.FileId, cancellationToken);

            var isAuthorized = file.HomeworkSolution.Student.UserId == currentUserId ||
                      file.HomeworkSolution.Homework.Lesson.ClassSchoolYearSubject.ClassSchoolYearSubjectTeachers.Any(x =>
                          x.Teacher.UserId == currentUserId);
            return isAuthorized
                ? RequestAuthorizationResult.Success
                : RequestAuthorizationResult.Failure(
                    "A beadott fájlokat csak a beadó diák és a javító tanárok tekinthetik meg.");
        }
    }
}