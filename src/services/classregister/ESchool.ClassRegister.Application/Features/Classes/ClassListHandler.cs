using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.ClassRegister.Interface.Features.Classes;
using ESchool.Libs.Application.Cqrs.Handlers;

namespace ESchool.ClassRegister.Application.Features.Classes
{
    public class ClassListHandler : AutoMapperPagedListHandler<ClassListQuery, Class, ClassListResponse>
    {
        public ClassListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }

        protected override IQueryable<Class> Filter(IQueryable<Class> entities, ClassListQuery query)
        {
            return entities.Where(x => query.IncludeFinishedClasses || !x.DidFinish);
        }

        protected override IOrderedQueryable<Class> Order(IQueryable<Class> entities, ClassListQuery query)
        {
            return entities.OrderBy(x => x.ClassSchoolYears.Count + x.ClassType.StartingGrade - 1)
                .ThenBy(x => x.ClassType.Name);
        }
    }
}