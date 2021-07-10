using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Application.Features.Users.Common;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Users;
using ESchool.ClassRegister.Interface.Features.Users;
using ESchool.ClassRegister.Interface.Features.Users.Teachers;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.ClassRegister.Application.Features.Users.Teachers
{
    public class TeacherListHandler : AutoMapperPagedListHandler<TeacherListQuery, Teacher, UserRoleListResponse>
    {
        public TeacherListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }

        protected override IQueryable<Teacher> Filter(IQueryable<Teacher> entities, TeacherListQuery query)
        {
            return entities.Where(x => x.User.Name.ToLower().Contains(query.SearchText.ToLower()));
        }

        protected override IOrderedQueryable<Teacher> Order(IQueryable<Teacher> entities)
        {
            return entities.OrderBy(x => x.User.Name);
        }
    }
}