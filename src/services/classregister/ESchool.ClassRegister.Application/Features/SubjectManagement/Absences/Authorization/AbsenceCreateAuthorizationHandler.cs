using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Absences.Authorization
{
    public class AbsenceCreateAuthorizationHandler : IRequestAuthorizationHandler<AbsenceCreateCommand>
    {
        private readonly ClassRegisterContext context;
        private readonly IIdentityService identityService;

        public AbsenceCreateAuthorizationHandler(ClassRegisterContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(AbsenceCreateCommand request, CancellationToken cancellationToken)
        {
            var lesson = await context.Lessons.Include(x => x.ClassSchoolYearSubject)
                    .ThenInclude(x => x.ClassSchoolYearSubjectTeachers)
                        .ThenInclude(x => x.Teacher)
                .SingleAsync(x => x.Id == request.LessonId, cancellationToken);
            
            var currentUserId = identityService.GetCurrentUserId();
            if (lesson.ClassSchoolYearSubject.ClassSchoolYearSubjectTeachers.All(x => x.Teacher.UserId != currentUserId))
            {
                return RequestAuthorizationResult.Failure(
                    "Csak olyan tanárok vehetnek fel hiányzást, akik tanítják az adott tárgyat, az osztálynak a tanévben.");
            }
            
            return RequestAuthorizationResult.Success;
        }
    }
}