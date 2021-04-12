using System;
using System.Collections.Generic;
using ESchool.Libs.Domain.Enums;

namespace ESchool.IdentityProvider.Interface.IntegrationEvents.TenantUsers
{
    public class TenantUserCreatedOrEditedEvent
    {
        public string Email { get; set; }
        public Guid TenantId { get; set; }
        public Guid UserId { get; set; }
        public List<TenantRoleType> TenantRoles { get; set; }
    }
}