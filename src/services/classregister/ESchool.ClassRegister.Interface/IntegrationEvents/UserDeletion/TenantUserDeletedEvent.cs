using System;

namespace ESchool.ClassRegister.Interface.IntegrationEvents.UserDeletion
{
    public abstract class TenantUserDeletedEvent
    {
        public Guid Id { get; set; }
    }
}