using System;
using System.Linq;
using ESchool.Libs.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Libs.Domain.Extensions
{
    public static class DbContextExtensions
    {
        public static void SetTenantId(this DbContext dbContext, Guid tenantId)
        {
            var entries = dbContext.ChangeTracker.Entries<IMultiTenantEntity>()
                .Where(x => x.State == EntityState.Added);

            foreach (var entry in entries)
            {
                entry.Entity.TenantId = tenantId;
            }
        }

        public static void SoftDelete(this DbContext dbContext)
        {
            var entries = dbContext.ChangeTracker.Entries<ISoftDelete>()
                .Where(x => x.State == EntityState.Deleted);

            foreach (var entry in entries)
            {
                entry.Entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}