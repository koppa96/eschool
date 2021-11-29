using AutoMapper;
using ESchool.IdentityProvider.Domain.Entities;
using ESchool.IdentityProvider.Interface.Features.Tenants;

namespace ESchool.IdentityProvider.Application.Features.Tenants.Common
{
    public class TenantMappings : Profile
    {
        public TenantMappings()
        {
            CreateMap<Tenant, TenantListResponse>();
        }
    }
}