using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESchool.Libs.Application.Cqrs.Query;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Libs.Application.Cqrs.Handlers
{
    public abstract class AutoMapperPagedListHandler<TQuery, TEntity, TOrderBy, TResponse> : PagedListHandler<TQuery, TEntity, TOrderBy, TResponse>
        where TQuery : PagedListQuery<TResponse>
        where TEntity : class
    {
        private readonly IConfigurationProvider configurationProvider;

        public AutoMapperPagedListHandler(DbContext context, IConfigurationProvider configurationProvider) : base(context)
        {
            this.configurationProvider = configurationProvider;
        }
        
        protected override IQueryable<TResponse> Map(IQueryable<TEntity> entities, TQuery query)
        {
            return entities.ProjectTo<TResponse>(configurationProvider);
        }
    }
}