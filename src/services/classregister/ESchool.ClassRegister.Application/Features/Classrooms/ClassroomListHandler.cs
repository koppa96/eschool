using System;
using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.ClassRegister.Interface.Features.Classrooms;
using ESchool.Libs.Application.Cqrs.Handlers;

namespace ESchool.ClassRegister.Application.Features.Classrooms
{
    public class ClassroomListHandler : AutoMapperPagedListHandler<ClassroomListQuery, Classroom, ClassroomListResponse>
    {
        public ClassroomListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }

        protected override IQueryable<Classroom> Filter(IQueryable<Classroom> entities, ClassroomListQuery query)
        {
            return !string.IsNullOrEmpty(query.SearchText)
                ? entities.Where(x => x.Name.ToLower().Contains(query.SearchText.ToLower()))
                : entities;
        }

        protected override IOrderedQueryable<Classroom> Order(IQueryable<Classroom> entities)
        {
            return entities.OrderBy(x => x.Name);
        }
    }
}