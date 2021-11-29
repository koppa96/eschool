using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.Absences;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Absences
{
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

        protected override IQueryable<Absence> Filter(IQueryable<Absence> entities, AbsenceListQuery query)
        {
            return entities.Where(x => x.StudentId == query.StudentId &&
                                       x.Lesson.ClassSchoolYearSubject.ClassSchoolYear.SchoolYearId ==
                                       query.SchoolYearId);
        }

        protected override IOrderedQueryable<Absence> Order(IQueryable<Absence> entities, AbsenceListQuery query)
        {
            return entities.OrderByDescending(x => x.Lesson.StartsAt);
        }
    }
}