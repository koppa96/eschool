using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.SubjectTeachers;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.SubjectTeachers
{
    public class SubjectTeacherListHandler : AutoMapperPagedListHandler<SubjectTeacherListQuery, SubjectTeacher, UserRoleListResponse>
    {
        public SubjectTeacherListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }

        protected override IOrderedQueryable<SubjectTeacher> Order(IQueryable<SubjectTeacher> entities, SubjectTeacherListQuery query)
        {
            return entities.OrderBy(x => x.Teacher.User.Name);
        }
    }
}