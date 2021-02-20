using AutoMapper;
using ESchool.IdentityProvider.Domain.Entities;

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