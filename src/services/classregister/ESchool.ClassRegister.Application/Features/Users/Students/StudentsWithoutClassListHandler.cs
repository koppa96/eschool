using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Application.Features.Users.Common;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Users;
using ESchool.ClassRegister.Interface.Features.Users;
using ESchool.ClassRegister.Interface.Features.Users.Students;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.ClassRegister.Application.Features.Users.Students
{
    public class StudentsWithoutClassListHandler : AutoMapperPagedListHandler<StudentsWithoutClassListQuery, Student, UserRoleListResponse>
    {
        public StudentsWithoutClassListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }

        protected override IQueryable<Student> Filter(IQueryable<Student> entities, StudentsWithoutClassListQuery query)
        {
            return entities.Where(x => x.Class == null && x.User.Name.ToLower().Contains(query.SearchText.ToLower()));
        }

        protected override IOrderedQueryable<Student> Order(IQueryable<Student> entities, StudentsWithoutClassListQuery query)
        {
            return entities.OrderBy(x => x.User.Name);
        }
    }
}