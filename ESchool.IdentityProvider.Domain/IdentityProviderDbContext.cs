using ESchool.IdentityProvider.Domain.Roles;
using ESchool.IdentityProvider.Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.IdentityProvider.Domain
{
    public class IdentityProviderDbContext : IdentityDbContext<User, Role, Guid>
    {
        public IdentityProviderDbContext(DbContextOptions<IdentityProviderDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TenantUser>()
                .HasBaseType<User>();
        }
    }
}
