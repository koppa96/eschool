using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Domain.Interfaces;
using ESchool.Libs.Domain.MultiTenancy.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

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

        public static async Task CreationAuditAsync<TUser, TUserRole>(
            this DbContext dbContext,
            Guid currentUserId,
            CancellationToken cancellationToken = default
        )
            where TUser : UserBase<TUser, TUserRole>
            where TUserRole : UserRoleBase<TUser, TUserRole>
        {
            var entries = dbContext.ChangeTracker.Entries<ICreationAuditedEntity<TUser, TUserRole>>();

            var user = await dbContext.Set<TUser>()
                .FindOrThrowAsync(currentUserId, cancellationToken);
            
            foreach (var entry in entries)
            {
                entry.Entity.CreatedAt = DateTime.Now;
                entry.Entity.CreatedBy = user;
            }
        }

        public static async Task ModificationAuditAsync<TUser, TUserRole>(
            this DbContext dbContext,
            Guid currentUserId,
            CancellationToken cancellationToken = default
        )
            where TUser : UserBase<TUser, TUserRole>
            where TUserRole : UserRoleBase<TUser, TUserRole>
        {
            var entries = dbContext.ChangeTracker.Entries<IModificationAuditedEntity<TUser, TUserRole>>();

            var user = await dbContext.Set<TUser>()
                .FindOrThrowAsync(currentUserId, cancellationToken);
            
            foreach (var entry in entries)
            {
                entry.Entity.LastModifiedAt = DateTime.Now;
                entry.Entity.LastModifiedBy = user;
            }
        }
    }
}