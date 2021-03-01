using System.Linq;
using AutoMapper;
using ESchool.IdentityProvider.Application.Features.Users.Common;
using ESchool.IdentityProvider.Domain.Entities.Users;

namespace ESchool.IdentityProvider.Application.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<User, UserDetailsResponse>()
                .ForMember(x => x.Tenants, o => o.MapFrom(x => x.TenantUsers.Select(u => u.Tenant)));

            CreateMap<User, UserListResponse>();
        }
    }
}