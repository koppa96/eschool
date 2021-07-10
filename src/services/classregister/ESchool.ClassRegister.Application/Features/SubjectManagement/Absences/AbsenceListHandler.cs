using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
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

        protected override IOrderedQueryable<Absence> Order(IQueryable<Absence> entities)
        {
            return entities.OrderByDescending(x => x.Lesson.StartsAt);
        }
    }
}