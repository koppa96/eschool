using ESchool.Libs.Domain.Interfaces;
using System;

namespace ESchool.IdentityProvider.Domain.Entities.Users
{
    public class TenantUser : User, IMultiTenantEntity
    {
        public Guid TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }
    }
}
