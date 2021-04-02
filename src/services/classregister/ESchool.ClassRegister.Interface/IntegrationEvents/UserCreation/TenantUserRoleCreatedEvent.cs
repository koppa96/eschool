using System;

namespace ESchool.ClassRegister.Interface.IntegrationEvents.UserCreation
{
    public abstract class TenantUserRoleCreatedEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
    }
}