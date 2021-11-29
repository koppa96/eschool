using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.Absences;
using ESchool.Libs.Application.Cqrs.Handlers;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Absences
{
    public class LessonAbsenceListHandler : AutoMapperPagedListHandler<LessonAbsenceListQuery, Absence, LessonAbsenceListResponse>
    {
        public LessonAbsenceListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider) :
            base(context, configurationProvider)
        {
        }

        protected override IQueryable<Absence> Filter(IQueryable<Absence> entities, LessonAbsenceListQuery query)
        {
            return entities.Where(x => x.LessonId == query.LessonId);
        }

        protected override IOrderedQueryable<Absence> Order(IQueryable<Absence> entities, LessonAbsenceListQuery query)
        {
            return entities.OrderBy(x => x.Student.User.Name);
        }
    }
}