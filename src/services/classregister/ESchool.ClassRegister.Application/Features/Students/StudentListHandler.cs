using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Users;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Interface.Query;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.ClassRegister.Application.Features.Students
{
    public class StudentListQuery : PagedListQuery<UserRoleListResponse>
    {
        public string SearchText { get; set; }
    }
    
    public class StudentListHandler : AutoMapperPagedListHandler<StudentListQuery, Student, UserRoleListResponse>
    {
        public StudentListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider)
            : base(context, configurationProvider)
        {
        }

        protected override IQueryable<Student> Filter(IQueryable<Student> entities, StudentListQuery query)
        {
            if (!string.IsNullOrEmpty(query.SearchText))
            {
                return entities.Where(x => x.User.Name.ToLower().Contains(query.SearchText.ToLower()));
            }

            return entities;
        }

        protected override IOrderedQueryable<Student> Order(IQueryable<Student> entities, StudentListQuery query)
        {
            return entities.OrderBy(x => x.User.Name);
        }
    }
}