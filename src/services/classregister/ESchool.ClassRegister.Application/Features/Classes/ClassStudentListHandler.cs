using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Users;
using ESchool.ClassRegister.Interface.Features.Classes;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.ClassRegister.Application.Features.Classes
{
    public class ClassStudentListHandler : AutoMapperPagedListHandler<ClassStudentListQuery, Student, UserRoleListResponse>
    {
        public ClassStudentListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }

        protected override IQueryable<Student> Filter(IQueryable<Student> entities, ClassStudentListQuery query)
        {
            return entities.Where(x =>
                x.ClassId == query.ClassId &&
                (string.IsNullOrEmpty(query.SearchText) || x.User.Name.ToLower().Contains(query.SearchText.ToLower())));
        }

        protected override IOrderedQueryable<Student> Order(IQueryable<Student> entities, ClassStudentListQuery query)
        {
            return entities.OrderBy(x => x.User.Name);
        }
    }
}