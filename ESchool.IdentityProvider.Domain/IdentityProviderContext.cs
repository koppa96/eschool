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
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Extensions;
using ESchool.Libs.Domain.Interfaces;

namespace ESchool.IdentityProvider.Domain
{
    public class IdentityProviderContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        private readonly bool isTenantAdmin;
        private readonly Guid? tenantId;

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantUser> TenantUsers { get; set; }
        public DbSet<TenantUserRole> TenantUserRoles { get; set; }
        
        public IdentityProviderContext(DbContextOptions<IdentityProviderContext> options, IIdentityService identityService) : base(options)
        {
            isTenantAdmin = identityService.IsInGlobalRole(GlobalRoleType.TenantAdministrator);
            tenantId = identityService.TryGetTenantId();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.AddGlobalQueryFilter<IMultiTenantEntity>(x => isTenantAdmin || x.TenantId == tenantId.Value);
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
