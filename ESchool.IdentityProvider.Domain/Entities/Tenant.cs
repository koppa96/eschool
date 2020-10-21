using System;
using System.Collections.Generic;
using ESchool.IdentityProvider.Domain.Entities.Users;

namespace ESchool.IdentityProvider.Domain.Entities
{
    public class Tenant
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string OfficialEmailAddress { get; set; }
        public string OmIdentifier { get; set; }
        public string HeadMaster { get; set; }

        public virtual ICollection<TenantUser> TenantUsers { get; set; }
    }
}