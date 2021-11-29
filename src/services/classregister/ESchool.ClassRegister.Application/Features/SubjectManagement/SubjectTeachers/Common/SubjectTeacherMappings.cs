using AutoMapper;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.SubjectTeachers.Common
{
    public class SubjectTeacherMappings : Profile
    {
        public SubjectTeacherMappings()
        {
            CreateMap<SubjectTeacher, UserRoleListResponse>()
                .ForMember(x => x.Id, o => o.MapFrom(x => x.TeacherId))
                .ForMember(x => x.Name, o => o.MapFrom(x => x.Teacher.User.Name));
        }
    }
}