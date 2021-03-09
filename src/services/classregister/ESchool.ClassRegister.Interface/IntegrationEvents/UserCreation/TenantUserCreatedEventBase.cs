using System;

namespace ESchool.ClassRegister.Interface.IntegrationEvents.UserCreation
{
    public class TenantUserCreatedEventBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
    }
}