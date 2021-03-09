using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.ClassRegister.Domain.Enums;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Application.Cqrs.Query;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Absences
{
    public class AbsenceListQuery : PagedListQuery<AbsenceListResponse>
    {
        public Guid StudentId { get; set; }
        public Guid SchoolYearId { get; set; }
    }

    public class AbsenceListResponse
    {
        public Guid Id { get; set; }
        public AbsenceState AbsenceState { get; set; }
        public LessonListResponse Lesson { get; set; }
    }

    public class AbsenceListHandler : AutoMapperPagedListHandler<AbsenceListQuery, Absence, AbsenceListResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IIdentityService identityService;

        public AbsenceListHandler(ClassRegisterContext context,
            IConfigurationProvider configurationProvider,
            IIdentityService identityService) : base(context, configurationProvider)
        {
            this.context = context;
            this.identityService = identityService;
        }

        protected override IOrderedQueryable<Absence> Order(IQueryable<Absence> entities)
        {
            return entities.OrderByDescending(x => x.Lesson.StartsAt);
        }

        protected override async Task ThrowIfCannotListAsync(AbsenceListQuery query, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            if (identityService.IsInRole(TenantRoleType.Administrator) ||
                currentUserId == query.StudentId)
            {
                return;
            }

            var student = await context.Students.Include(x => x.Class)
                .Include(x => x.StudentParents)
                .SingleAsync(x => x.Id == query.StudentId);

            if (student.Class.HeadTeacherId == currentUserId || student.StudentParents.Any(x => x.ParentId == currentUserId))
            {
                return;
            }

            throw new UnauthorizedAccessException(
                "Egy diák hiányzásait csak az osztályfőnök, a diák, a szülei és az adminisztrátorok tekinthetik meg.");
        }
    }
}