using System;
using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Users;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Interface.Query;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.ClassRegister.Application.Features.Classes
{
    public class ClassStudentListQuery : PagedListQuery<UserRoleListResponse>
    {
        public Guid ClassId { get; set; }
    }
    
    public class ClassStudentListHandler : AutoMapperPagedListHandler<ClassStudentListQuery, Student, UserRoleListResponse>
    {
        public ClassStudentListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }

        protected override IQueryable<Student> Filter(IQueryable<Student> entities, ClassStudentListQuery query)
        {
            return entities.Where(x => x.ClassId == query.ClassId);
        }

        protected override IOrderedQueryable<Student> Order(IQueryable<Student> entities, ClassStudentListQuery query)
        {
            return entities.OrderBy(x => x.User.Name);
        }
    }
}