using System;
using ESchool.Libs.Domain.Enums;

namespace ESchool.IdentityProvider.Domain.Entities.Users
{
    public class TenantUserRole
    {
        public Guid Id { get; set; }
        
        public Guid TenantUserId { get; set; }
        public virtual TenantUser TenantUser { get; set; }

        public TenantRoleType TenantRole { get; set; }
    }
}