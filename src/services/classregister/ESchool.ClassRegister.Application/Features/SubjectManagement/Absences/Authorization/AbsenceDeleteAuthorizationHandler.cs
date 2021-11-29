using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.Absences;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Absences.Authorization
{
    public class AbsenceDeleteAuthorizationHandler : IRequestAuthorizationHandler<AbsenceDeleteCommand>
    {
        private readonly IIdentityService identityService;
        private readonly ClassRegisterContext context;

        public AbsenceDeleteAuthorizationHandler(IIdentityService identityService, ClassRegisterContext context)
        {
            this.identityService = identityService;
            this.context = context;
        }
        
        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(AbsenceDeleteCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var absence = await context.Absences.Include(x => x.Lesson)
                    .ThenInclude(x => x.ClassSchoolYearSubject)
                        .ThenInclude(x => x.ClassSchoolYearSubjectTeachers)
                            .ThenInclude(x => x.Teacher)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);
            
            if (!identityService.IsInRole(TenantRoleType.Administrator) && 
                absence.Lesson.ClassSchoolYearSubject.ClassSchoolYearSubjectTeachers.All(x => x.Teacher.UserId != currentUserId))
            {
                return RequestAuthorizationResult.Failure("A hiányzást csak a tárgy oktatói vagy az adminisztrátorok törölhetik.");
            }
            
            return RequestAuthorizationResult.Success;
        }
    }
}