using ESchool.Frontend.Network.Abstractions;
using ESchool.Frontend.Network.IdentityProvider.Endpoints.Tenants.Users;

namespace ESchool.Frontend.Network.IdentityProvider.Endpoints.Tenants
{
    public class TenantsChildEndpointSelector
    {
        public TenantUsersEndpoint Users { get; set; }
        
        public TenantsChildEndpointSelector(string basePath, ChildEndpointFactory childEndpointFactory)
        {
            Users = childEndpointFactory
                .CreateChildEndpoint<TenantUsersEndpoint>(basePath + "/users");
        }
    }
}