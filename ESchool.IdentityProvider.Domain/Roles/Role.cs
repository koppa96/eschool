using ESchool.Libs.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.IdentityProvider.Domain.Roles
{
    public class Role : IdentityRole<Guid>
    {
        public Role(RoleTypes roleType)
        {
            Name = roleType.ToString();
        }
    }
}
