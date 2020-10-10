using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using ESchool.IdentityProvider.Domain.Entities;
using ESchool.IdentityProvider.Domain.Entities.Roles;
using ESchool.IdentityProvider.Domain.Entities.Users;

namespace ESchool.IdentityProvider.Domain
{
    public class IdentityProviderContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Tenant> Tenants { get; set; }
        
        public IdentityProviderContext(DbContextOptions<IdentityProviderContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
