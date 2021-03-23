using AutoMapper;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.HomeAssignments.Domain.Entities.Users;

namespace ESchool.HomeAssignments.Application.Features.Users.Common
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<HomeAssignmentsUserRole, UserListResponse>()
                .ForMember(x => x.Name, o => o.MapFrom(x => x.User.Name));
            
            CreateMap<StudentHomework, UserListResponse>()
                .ForMember(x => x.Id, o => o.MapFrom(x => x.StudentId))
                .ForMember(x => x.Name, o => o.MapFrom(x => x.Student.User.Name));

            CreateMap<TeacherHomework, UserListResponse>()
                .ForMember(x => x.Id, o => o.MapFrom(x => x.TeacherId))
                .ForMember(x => x.Name, o => o.MapFrom(x => x.Teacher.User.Name));
        }
    }
}