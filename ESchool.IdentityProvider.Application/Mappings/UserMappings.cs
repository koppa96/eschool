using System.Linq;
using AutoMapper;
using ESchool.IdentityProvider.Application.Features.Users.Common;
using ESchool.IdentityProvider.Domain.Entities.Users;
using ESchool.Libs.Application.Dtos;
using ESchool.Libs.Application.IntegrationEvents;

namespace ESchool.IdentityProvider.Application.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<User, UserDetailsResponse>()
                .ForMember(dest => dest.TenantRoles, opt => opt.MapFrom(src => src.TenantUsers.Select(x => new TenantRoleDto
                {
                    TenantId = x.Id,
                    Roles = x.TenantUserRoles.Select(r => new TenantRoleDto.TenantUserRoleDto
                    {
                        Id = r.Id,
                        TenantRoleType = r.TenantRole
                    })
                })));

            CreateMap<User, UserCreatedIntegrationEvent>()
                .ForMember(dest => dest.TenantRoles, opt => opt.MapFrom(src => src.TenantUsers.Select(x => new TenantRoleDto
                {
                    TenantId = x.Id,
                    Roles = x.TenantUserRoles.Select(r => new TenantRoleDto.TenantUserRoleDto
                    {
                        Id = r.Id,
                        TenantRoleType = r.TenantRole
                    })
                })));
        }
    }
}