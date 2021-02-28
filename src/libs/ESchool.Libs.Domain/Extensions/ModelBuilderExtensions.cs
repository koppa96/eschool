using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ESchool.Libs.Domain.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void AddGlobalQueryFilter<TEntity>(this ModelBuilder modelBuilder, Expression<Func<TEntity, bool>> filter)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                .Where(x => typeof(TEntity).IsAssignableFrom(x.ClrType) && x.BaseType == null))
            {
                var newParam = Expression.Parameter(entityType.ClrType);
                var newBody = ReplacingExpressionVisitor.Replace(filter.Parameters.Single(), newParam, filter.Body);

                modelBuilder.Entity(entityType.ClrType)
                    .HasQueryFilter(Expression.Lambda(newBody, newParam));
            }
        }
    }
}