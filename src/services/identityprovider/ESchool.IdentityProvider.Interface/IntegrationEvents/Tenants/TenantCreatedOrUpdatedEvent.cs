using System;

namespace ESchool.IdentityProvider.Interface.IntegrationEvents.Tenants
{
    public class TenantCreatedOrUpdatedEvent
    {
        public Guid TenantId { get; set; }
    }
}