using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Application.Cqrs.Query;
using ESchool.Libs.Application.Cqrs.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Libs.Application.Cqrs.Handlers
{
    public abstract class PagedListHandler<TQuery, TEntity, TOrderBy, TResponse> : IRequestHandler<TQuery, PagedListResponse<TResponse>>
        where TQuery : PagedListQuery<TResponse>
        where TEntity : class
    {
        private readonly DbContext context;
        
        protected abstract Expression<Func<TEntity, TOrderBy>> OrderBy { get; }

        protected PagedListHandler(DbContext context)
        {
            this.context = context;
        }
        
        protected virtual IQueryable<TEntity> Include(IQueryable<TEntity> entities)
        {
            return entities;
        }

        protected virtual IQueryable<TEntity> Filter(IQueryable<TEntity> entities, TQuery query)
        {
            return entities;
        }

        protected abstract IQueryable<TResponse> Map(IQueryable<TEntity> entities, TQuery query);

        public async Task<PagedListResponse<TResponse>> Handle(TQuery request, CancellationToken cancellationToken)
        {
            var dbSet = context.Set<TEntity>();
            var totalCount = await dbSet.CountAsync(cancellationToken);
            
            var responses = new List<TResponse>();
            if (totalCount > request.PageIndex * request.PageSize)
            {
                responses = await Map(Filter(Include(dbSet), request)
                    .OrderByDescending(OrderBy)
                    .Skip(request.PageIndex * request.PageSize)
                    .Take(request.PageSize), request)
                    .ToListAsync(cancellationToken);
            }

            return new PagedListResponse<TResponse>
            {
                Items = responses,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
        }
    }
}