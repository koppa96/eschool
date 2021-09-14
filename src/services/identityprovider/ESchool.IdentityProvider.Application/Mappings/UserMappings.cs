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
                .ForMember(x => x.Tenants, o => o.MapFrom(x => x.TenantUsers))
                .ForMember(x => x.GlobalRoleType, o => o.MapFrom(x => x.GlobalRole));

            CreateMap<TenantUser, UserDetailsResponse.TenantUserListResponse>()
                .ForMember(x => x.Id, o => o.MapFrom(x => x.Tenant.Id))
                .ForMember(x => x.Name, o => o.MapFrom(x => x.Tenant.Name))
                .ForMember(x => x.OmIdentifier, o => o.MapFrom(x => x.Tenant.OmIdentifier))
                .ForMember(x => x.TenantRoleTypes,
                    o => o.MapFrom(x => x.TenantUserRoles.Select(x => x.TenantRole).ToList()));

            CreateMap<User, UserListResponse>();
        }
    }
}