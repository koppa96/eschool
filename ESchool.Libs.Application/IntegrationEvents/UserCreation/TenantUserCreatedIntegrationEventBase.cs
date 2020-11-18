using System;

namespace ESchool.Libs.Application.IntegrationEvents.UserCreation
{
    public class TenantUserCreatedIntegrationEventBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}