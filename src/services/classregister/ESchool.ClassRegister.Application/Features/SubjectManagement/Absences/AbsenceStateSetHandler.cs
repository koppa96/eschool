using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Enums;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Extensions;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Absences
{
    public class AbsenceStateSetCommand
    {
        public AbsenceState AbsenceState { get; set; }
    }
    
    public class AbsenceStateSetHandler : IRequestHandler<EditCommand<AbsenceStateSetCommand>>
    {
        private readonly ClassRegisterContext context;
        private readonly IIdentityService identityService;

        public AbsenceStateSetHandler(ClassRegisterContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<Unit> Handle(EditCommand<AbsenceStateSetCommand> request, CancellationToken cancellationToken)
        {
            var absence = await context.Absences.Include(x => x.Lesson)
                    .ThenInclude(x => x.ClassSchoolYearSubject)
                        .ThenInclude(x => x.ClassSchoolYear)
                            .ThenInclude(x => x.Class)
                    .SingleAsync(x => x.Id == request.Id, cancellationToken);

            if (!identityService.IsInRole(TenantRoleType.Administrator) && identityService.GetCurrentUserId() !=
                absence.Lesson.ClassSchoolYearSubject.ClassSchoolYear.Class.HeadTeacherId)
            {
                throw new UnauthorizedAccessException(
                    "Egy hiányzás állapotát csak egy adminisztrátor vagy az osztályfőnök állíthatja.");
            }

            absence.AbsenceState = request.InnerCommand.AbsenceState;
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}