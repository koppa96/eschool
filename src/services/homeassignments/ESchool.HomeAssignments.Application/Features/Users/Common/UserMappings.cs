using AutoMapper;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.HomeAssignments.Domain.Entities.Users;

namespace ESchool.HomeAssignments.Application.Features.Users.Common
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<UserBase, UserListResponse>();
            CreateMap<StudentHomework, UserListResponse>()
                .ForMember(x => x.Id, o => o.MapFrom(x => x.StudentId))
                .ForMember(x => x.Name, o => o.MapFrom(x => x.Student.Name));

            CreateMap<TeacherHomework, UserListResponse>()
                .ForMember(x => x.Id, o => o.MapFrom(x => x.TeacherId))
                .ForMember(x => x.Name, o => o.MapFrom(x => x.Teacher.Name));
        }
    }
}