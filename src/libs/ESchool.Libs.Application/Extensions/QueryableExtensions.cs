using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ESchool.Libs.Interface.Enums;
using ESchool.Libs.Interface.Query;

namespace ESchool.Libs.Application.Extensions
{
    public static class QueryableExtensions
    {
        public static IOrderedQueryable<TEntity> OrderBy<TEntity, TResponse>(this IQueryable<TEntity> queryable,
            OrderedPagedListQuery<TResponse> query)
        {
            if (query.Orderings?.Any() != true)
            {
                throw new ArgumentException("The proveded query specifies no orderings.", nameof(query));
            }

            var firstOrdering = query.Orderings.First();
            var orderByExpression = CreateOrderingExpression<TEntity>(firstOrdering);
            var orderedQueryable = firstOrdering.Value == OrderingDirection.Ascending
                ? queryable.OrderBy(orderByExpression)
                : queryable.OrderByDescending(orderByExpression);

            foreach (var ordering in query.Orderings.Skip(1))
            {
                var thenByExpression = CreateOrderingExpression<TEntity>(ordering);
                orderedQueryable = ordering.Value == OrderingDirection.Ascending
                    ? orderedQueryable.ThenBy(thenByExpression)
                    : orderedQueryable.ThenByDescending(thenByExpression);
            }

            return orderedQueryable;
        }

        private static Expression<Func<TEntity, object>> CreateOrderingExpression<TEntity>(
            KeyValuePair<string, OrderingDirection> orderingDescriptor)
        {
            var parameter = Expression.Parameter(typeof(TEntity));
            var body = Expression.Property(parameter, orderingDescriptor.Key);

            return Expression.Lambda<Func<TEntity, object>>(body, parameter);
        }
    }
}