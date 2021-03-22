using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Domain.Interfaces;
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

        private static async Task<IUser> GetUserAsync(DbContext dbContext, Type userType, Guid currentUserId,
            CancellationToken cancellationToken)
        {
            Expression<Func<IUser, bool>> expression = x => x.UserId == currentUserId;
            var dbSet = typeof(DbContext).GetMethod(nameof(DbContext.Set))
                .MakeGenericMethod(userType)
                .Invoke(dbContext, Array.Empty<object>());
                    
            var newParam = Expression.Parameter(userType);
            var newBody = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), newParam, expression.Body);

            var userTask = (Task) typeof(EntityFrameworkQueryableExtensions)
                .GetMethod(nameof(EntityFrameworkQueryableExtensions.SingleAsync))
                .MakeGenericMethod(userType)
                .Invoke(dbSet, new object[] { Expression.Lambda(newBody, newParam), cancellationToken });
            await userTask;
            return (IUser) typeof(Task<>).MakeGenericType(userType)
                .GetProperty(nameof(Task<object>.Result))
                .GetValue(userTask);
        }

        public static async Task CreationAuditAsync(this DbContext dbContext, Guid currentUserId, CancellationToken cancellationToken = default)
        {
            var entries = dbContext.ChangeTracker.Entries()
                .Where(x => x.Entity.GetType().GetInterfaces().Any(i =>
                                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICreationAuditedEntity<>)) &&
                            x.State == EntityState.Added);

            var valueCache = new Dictionary<Type, IUser>();
            
            foreach (var entry in entries)
            {
                var userType = entry.Entity.GetType().GetInterfaces().Single(i =>
                        i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICreationAuditedEntity<>))
                    .GetGenericArguments()
                    .Single();

                if (valueCache.ContainsKey(userType))
                {
                    var entity = (ICreationAuditedEntity) entry.Entity;
                    entity.SetCreation(valueCache[userType]);
                }
                else
                {
                    var user = await GetUserAsync(dbContext, userType, currentUserId, cancellationToken);
                    valueCache.Add(userType, user);
                    var entity = (ICreationAuditedEntity) entry.Entity;
                    entity.SetCreation(user);
                }
            }
        }

        public static async Task ModificationAuditAsync(this DbContext dbContext, Guid currentUserId,
            CancellationToken cancellationToken = default)
        {
            var entries = dbContext.ChangeTracker.Entries()
                .Where(x => x.Entity.GetType().GetInterfaces().Any(i =>
                                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IModificationAuditedEntity<>)) &&
                            x.State == EntityState.Modified);

            var valueCache = new Dictionary<Type, IUser>();
            
            foreach (var entry in entries)
            {
                var userType = entry.Entity.GetType().GetInterfaces().Single(i =>
                        i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IModificationAuditedEntity<>))
                    .GetGenericArguments()
                    .Single();

                if (valueCache.ContainsKey(userType))
                {
                    var entity = (IModificationAuditedEntity) entry.Entity;
                    entity.SetModification(valueCache[userType]);
                }
                else
                {
                    var user = await GetUserAsync(dbContext, userType, currentUserId, cancellationToken);
                    valueCache.Add(userType, user);
                    var entity = (IModificationAuditedEntity) entry.Entity;
                    entity.SetModification(user);
                }
            }
        }
    }
}