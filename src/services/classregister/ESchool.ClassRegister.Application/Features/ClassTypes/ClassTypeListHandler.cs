using System;
using System.Linq.Expressions;
using AutoMapper;
using ESchool.ClassRegister.Application.Features.ClassTypes.Common;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Application.Cqrs.Query;

namespace ESchool.ClassRegister.Application.Features.ClassTypes
{
    public class ClassTypeListQuery : PagedListQuery<ClassTypeListResponse>
    {
    }
    
    public class ClassTypeListHandler : AutoMapperPagedListHandler<ClassTypeListQuery, ClassType, string, ClassTypeListResponse>
    {
        public ClassTypeListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }

        protected override Expression<Func<ClassType, string>> OrderBy => x => x.Name;
    }
}