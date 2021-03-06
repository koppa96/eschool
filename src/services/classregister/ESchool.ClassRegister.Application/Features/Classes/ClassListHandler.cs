﻿using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Application.Features.Classes.Common;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Application.Cqrs.Query;

namespace ESchool.ClassRegister.Application.Features.Classes
{
    public class ClassListQuery : PagedListQuery<ClassListResponse>
    {
        public bool IncludeFinishedClasses { get; set; }
    }
    
    public class ClassListHandler : AutoMapperPagedListHandler<ClassListQuery, Class, ClassListResponse>
    {
        public ClassListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }

        protected override IQueryable<Class> Filter(IQueryable<Class> entities, ClassListQuery query)
        {
            return entities.Where(x => query.IncludeFinishedClasses || x.DidFinish);
        }

        protected override IOrderedQueryable<Class> Order(IQueryable<Class> entities)
        {
            return entities.OrderBy(x => x.ClassSchoolYears.Count + x.ClassType.StartingGrade - 1);
        }
    }
}