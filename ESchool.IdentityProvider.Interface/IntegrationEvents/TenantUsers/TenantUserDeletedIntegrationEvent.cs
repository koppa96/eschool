using System;

namespace ESchool.IdentityProvider.Interface.IntegrationEvents.TenantUsers
{
    public class TenantUserDeletedIntegrationEvent
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
    }
}