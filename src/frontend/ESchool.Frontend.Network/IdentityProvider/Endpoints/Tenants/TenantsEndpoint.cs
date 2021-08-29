using System;
using ESchool.Frontend.Network.Abstractions;
using ESchool.IdentityProvider.Interface.Features.Tenants;
using Flurl.Http;

namespace ESchool.Frontend.Network.IdentityProvider.Endpoints.Tenants
{
    public class TenantsEndpoint : PagingCrudEndpoint<TenantListResponse, TenantDetailsResponse, CreateTenantCommand, EditTenantCommand>
    {
        private readonly ChildEndpointFactory childEndpointFactory;
        protected override string BasePath { get; }

        public TenantsChildEndpointSelector this[Guid tenantId] =>
            childEndpointFactory.CreateChildEndpointSelector<TenantsChildEndpointSelector>(BasePath + $"/{tenantId}");
        
        public TenantsEndpoint(string basePath, IFlurlClient flurlClient, ChildEndpointFactory childEndpointFactory) : base(flurlClient)
        {
            this.childEndpointFactory = childEndpointFactory;
            BasePath = basePath;
        }
    }
}