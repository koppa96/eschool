using System;
using System.Linq.Expressions;
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
    
    public class ClassTypeListHandler : PagedListHandler<ClassTypeListQuery, ClassType, string, ClassTypeListResponse>
    {
        public ClassTypeListHandler(ClassRegisterContext context) : base(context)
        {
        }

        protected override Expression<Func<ClassType, string>> OrderBy => x => x.Name;

        protected override Expression<Func<ClassType, ClassTypeListResponse>> Select => x => new ClassTypeListResponse
        {
            Id = x.Id,
            Name = x.Name
        };
    }
}