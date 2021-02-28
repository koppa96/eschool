using System;

namespace ESchool.IdentityProvider.Interface.IntegrationEvents.Tenants
{
    public class TenantDeletedEvent
    {
        public Guid TenantId { get; set; }
    }
}