using System;
using System.Linq;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Absences
{
    public class AbsenceDeleteCommand : DeleteCommand
    {
    }
    
    public class AbsenceDeleteHandler : DeleteHandler<DeleteCommand, Absence>
    {
        private readonly IIdentityService identityService;

        public AbsenceDeleteHandler(ClassRegisterContext context, IIdentityService identityService) : base(context)
        {
            this.identityService = identityService;
        }

        protected override IQueryable<Absence> Include(IQueryable<Absence> entities)
        {
            return entities.Include(x => x.Lesson)
                    .ThenInclude(x => x.ClassSchoolYearSubject)
                        .ThenInclude(x => x.ClassSchoolYearSubjectTeachers)
                .Include(x => x.Lesson);
        }

        protected override Task ThrowIfCannotDeleteAsync(Absence entity)
        {
            var currentUserId = identityService.GetCurrentUserId();
            if (!identityService.IsInRole(TenantRoleType.Administrator) && 
                entity.Lesson.ClassSchoolYearSubject.ClassSchoolYearSubjectTeachers.All(x => x.Id != currentUserId))
            {
                throw new UnauthorizedAccessException(
                    "A hiányzást csak a tárgy oktatói vagy az adminisztrátorok törölhetik.");
            }
            return Task.CompletedTask;
        }
    }
}