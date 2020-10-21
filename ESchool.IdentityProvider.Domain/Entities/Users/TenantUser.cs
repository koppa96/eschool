using ESchool.Libs.Domain.Interfaces;
using System;
using System.Collections.Generic;
using ESchool.Libs.Domain;

namespace ESchool.IdentityProvider.Domain.Entities.Users
{
    public class TenantUser
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }

        public virtual ICollection<TenantUserRole> TenantUserRoles { get; set; }
    }
}
