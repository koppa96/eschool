using AutoMapper;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.HomeAssignments.Domain.Entities.Users;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.HomeAssignments.Application.Features.Users.Common
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<HomeAssignmentsUserRole, UserRoleListResponse>()
                .ForMember(x => x.Name, o => o.MapFrom(x => x.User.Name));
        }
    }
}