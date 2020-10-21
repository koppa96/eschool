using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using ESchool.Libs.Domain.Enums;

namespace ESchool.IdentityProvider.Domain.Entities.Users
{
    public class User : IdentityUser<Guid>
    {
        public GlobalRoleType GlobalRole { get; set; }
        public virtual ICollection<TenantUser> TenantUsers { get; set; }
    }
}
