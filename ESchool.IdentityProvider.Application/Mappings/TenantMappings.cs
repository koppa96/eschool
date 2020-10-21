using AutoMapper;
using ESchool.IdentityProvider.Application.Features.Tenants;
using ESchool.IdentityProvider.Application.Features.Tenants.Common;
using ESchool.IdentityProvider.Domain.Entities;

namespace ESchool.IdentityProvider.Application.Mappings
{
    public class TenantMappings : Profile
    {
        public TenantMappings()
        {
            CreateMap<CreateTenantCommand, Tenant>();
            CreateMap<EditTenantCommand, Tenant>();

            CreateMap<Tenant, TenantDetailsResponse>();
            CreateMap<Tenant, TenantListResponse>();
        }
    }
}