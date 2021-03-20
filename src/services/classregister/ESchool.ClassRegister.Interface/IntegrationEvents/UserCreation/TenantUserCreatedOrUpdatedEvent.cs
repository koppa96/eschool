using System;

namespace ESchool.ClassRegister.Interface.IntegrationEvents.UserCreation
{
    public abstract class TenantUserCreatedOrUpdatedEvent
    {
        public Guid Id { get; set; }
    }
}