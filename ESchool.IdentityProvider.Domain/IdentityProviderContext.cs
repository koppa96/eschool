using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using ESchool.IdentityProvider.Domain.Entities;
using ESchool.IdentityProvider.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using ESchool.Libs.Domain.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ESchool.IdentityProvider.Domain
{
    public class IdentityProviderContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        private readonly IIdentityService identityService;

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantUser> TenantUsers { get; set; }
        
        public IdentityProviderContext(DbContextOptions<IdentityProviderContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            EntityAudit();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            EntityAudit();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void EntityAudit()
        {
            // TODO: Implement auditing methods
        }
    }
}
