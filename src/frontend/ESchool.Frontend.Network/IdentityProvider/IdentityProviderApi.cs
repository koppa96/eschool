using ESchool.Frontend.Network.Abstractions;
using ESchool.Frontend.Network.IdentityProvider.Endpoints;
using ESchool.Frontend.Network.IdentityProvider.Endpoints.Tenants;

namespace ESchool.Frontend.Network.IdentityProvider
{
    public class IdentityProviderApi
    {
        public const string BasePath = Api.BasePath + "/identity-provider";

        public TenantsEndpoint Tenants { get; }

        public IdentityProviderApi(ChildEndpointFactory childEndpointFactory)
        {
            Tenants = childEndpointFactory.CreateChildEndpoint<TenantsEndpoint>(BasePath + "/tenants");
        }
    }
}