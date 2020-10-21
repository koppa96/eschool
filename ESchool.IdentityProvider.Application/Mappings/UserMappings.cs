using System.Linq;
using AutoMapper;
using ESchool.IdentityProvider.Application.Features.Users.Common;
using ESchool.IdentityProvider.Domain.Entities.Users;
using ESchool.Libs.Application.IntegrationEvents;
using ESchool.Libs.Domain.Model;

namespace ESchool.IdentityProvider.Application.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<User, UserDetailsResponse>()
                .ForMember(dest => dest.TenantRoles, opt => opt.MapFrom(src => src.TenantUsers.Select(x => new TenantRole
                {
                    TenantId = x.Id,
                    Roles = x.TenantUserRoles.Select(r => r.TenantRole)
                })));

            CreateMap<User, UserCreatedIntegrationEvent>()
                .ForMember(dest => dest.TenantRoles, opt => opt.MapFrom(src => src.TenantUsers.Select(x => new TenantRole
                {
                    TenantId = x.Id,
                    Roles = x.TenantUserRoles.Select(r => r.TenantRole)
                })));
        }
    }
}