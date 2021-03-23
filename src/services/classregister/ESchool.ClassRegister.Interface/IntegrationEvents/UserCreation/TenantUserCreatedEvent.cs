using System;

namespace ESchool.ClassRegister.Interface.IntegrationEvents.UserCreation
{
    public abstract class TenantUserCreatedEvent
    {
        public Guid Id { get; set; }
    }
}