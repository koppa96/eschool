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
        }
    }
}