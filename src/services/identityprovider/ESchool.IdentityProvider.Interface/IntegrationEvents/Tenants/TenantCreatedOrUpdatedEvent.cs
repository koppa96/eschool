using System;

namespace ESchool.IdentityProvider.Interface.IntegrationEvents.Tenants
{
    public class TenantCreatedOrUpdatedEvent
    {
        public Guid TenantId { get; set; }
        public string Name { get; set; }
        public string OmIdentifier { get; set; }
    }
}