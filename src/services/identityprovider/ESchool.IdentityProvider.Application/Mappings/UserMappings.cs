using System.Linq;
using AutoMapper;
using ESchool.IdentityProvider.Domain.Entities.Users;
using ESchool.IdentityProvider.Interface.Features.Users;

namespace ESchool.IdentityProvider.Application.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<User, UserDetailsResponse>()
                .ForMember(x => x.Tenants, o => o.MapFrom(x => x.TenantUsers.Select(u => u.Tenant)))
                .ForMember(x => x.GlobalRoleType, o => o.MapFrom(x => x.GlobalRole));

            CreateMap<User, UserListResponse>();
        }
    }
}