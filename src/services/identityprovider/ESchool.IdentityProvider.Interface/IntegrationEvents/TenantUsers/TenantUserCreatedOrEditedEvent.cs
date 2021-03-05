using System;

namespace ESchool.IdentityProvider.Interface.IntegrationEvents.TenantUsers
{
    public class TenantUserCreatedOrEditedEvent
    {
        public Guid TenantId { get; set; }
        public Guid UserId { get; set; }
    }
}