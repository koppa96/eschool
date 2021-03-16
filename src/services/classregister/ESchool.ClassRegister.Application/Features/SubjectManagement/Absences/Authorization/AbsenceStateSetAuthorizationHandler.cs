using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Absences.Authorization
{
    public class AbsenceStateSetAuthorizationHandler : IRequestAuthorizationHandler<EditCommand<AbsenceStateSetCommand>>
    {
        private readonly ClassRegisterContext context;
        private readonly IIdentityService identityService;

        public AbsenceStateSetAuthorizationHandler(ClassRegisterContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(EditCommand<AbsenceStateSetCommand> request,
            CancellationToken cancellationToken)
        {
            var absence = await context.Absences.Include(x => x.Lesson)
                    .ThenInclude(x => x.ClassSchoolYearSubject)
                        .ThenInclude(x => x.ClassSchoolYear)
                            .ThenInclude(x => x.Class)
                                .ThenInclude(x => x.HeadTeacher)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);
            
            if (!identityService.IsInRole(TenantRoleType.Administrator) &&
                identityService.GetCurrentUserId() != absence.Lesson.ClassSchoolYearSubject.ClassSchoolYear.Class.HeadTeacher.UserId)
            {
                return RequestAuthorizationResult.Failure("Egy hiányzás állapotát csak egy adminisztrátor vagy az osztályfőnök állíthatja.");
            }
            
            return RequestAuthorizationResult.Success;
        }
    }
}