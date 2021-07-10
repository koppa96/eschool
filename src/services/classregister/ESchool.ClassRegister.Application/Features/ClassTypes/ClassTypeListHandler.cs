using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.ClassRegister.Interface.Features.ClassTypes;
using ESchool.Libs.Application.Cqrs.Handlers;

namespace ESchool.ClassRegister.Application.Features.ClassTypes
{
    public class ClassTypeListHandler : AutoMapperPagedListHandler<ClassTypeListQuery, ClassType, ClassTypeListResponse>
    {
        public ClassTypeListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }
        
        protected override IOrderedQueryable<ClassType> Order(IQueryable<ClassType> entities)
        {
            return entities.OrderBy(x => x.Name);
        }
    }
}