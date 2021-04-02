using System;

namespace ESchool.ClassRegister.Interface.IntegrationEvents.UserDeletion
{
    public abstract class TenantUserRoleDeletedEvent
    {
        public Guid Id { get; set; }
    }
}