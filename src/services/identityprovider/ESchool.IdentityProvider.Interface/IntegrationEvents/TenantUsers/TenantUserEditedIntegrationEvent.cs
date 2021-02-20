using System;
using System.Collections.Generic;
using ESchool.Libs.Domain.Enums;

namespace ESchool.IdentityProvider.Interface.IntegrationEvents.TenantUsers
{
    public class TenantUserEditedIntegrationEvent
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public Guid TenantId { get; set; }
        public List<TenantRoleType> TenantRoleTypes { get; set; }
    }
}