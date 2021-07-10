using System;

namespace ESchool.IdentityProvider.Interface.Features.Tenants
{
    public class TenantListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string OmIdentifier { get; set; }
    }
}