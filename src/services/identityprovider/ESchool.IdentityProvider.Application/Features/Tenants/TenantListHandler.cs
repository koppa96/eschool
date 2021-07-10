using System.Linq;
using AutoMapper;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Domain.Entities;
using ESchool.IdentityProvider.Interface.Features.Tenants;
using ESchool.Libs.Application.Cqrs.Handlers;

namespace ESchool.IdentityProvider.Application.Features.Tenants
{
    public class TenantListHandler : AutoMapperPagedListHandler<TenantListQuery, Tenant, TenantListResponse>
    {
        public TenantListHandler(IdentityProviderContext context, IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }
        
        protected override IOrderedQueryable<Tenant> Order(IQueryable<Tenant> entities)
        {
            return entities.OrderBy(x => x.Name);
        }
    }
}