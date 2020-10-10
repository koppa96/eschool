using ESchool.Libs.Domain;
using Microsoft.AspNetCore.Identity;
using System;

namespace ESchool.IdentityProvider.Domain.Entities.Roles
{
    public class Role : IdentityRole<Guid>
    {
        private Role()
        {
        }
        
        public Role(RoleTypes roleType)
        {
            Name = roleType.ToString();
        }
    }
}
