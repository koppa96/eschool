using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.ClassRegister.Interface.Features.Classes;
using ESchool.ClassRegister.Interface.Features.ClassSchoolYears;
using ESchool.Libs.Application.Cqrs.Handlers;

namespace ESchool.ClassRegister.Application.Features.ClassSchoolYears
{
    public class ClassSchoolYearListHandler : AutoMapperPagedListHandler<ClassSchoolYearListQuery, Class, ClassListResponse>
    {
        public ClassSchoolYearListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider)
            : base(context, configurationProvider)
        {
        }

        protected override IQueryable<Class> Filter(IQueryable<Class> entities, ClassSchoolYearListQuery query)
        {
            return entities.Where(x => x.ClassSchoolYears.Any(csy => csy.SchoolYearId == query.SchoolYearId));
        }

        protected override IOrderedQueryable<Class> Order(IQueryable<Class> entities, ClassSchoolYearListQuery query)
        {
            return entities.OrderBy(x => x.ClassType.StartingGrade + x.ClassSchoolYears.Count)
                .ThenBy(x => x.ClassType.Name);
        }
    }
}