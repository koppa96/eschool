using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESchool.Libs.Interface.Query;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Libs.Application.Cqrs.Handlers
{
    public abstract class AutoMapperPagedListHandler<TQuery, TEntity, TResponse> : PagedListHandler<TQuery, TEntity, TResponse>
        where TQuery : PagedListQuery<TResponse>
        where TEntity : class
    {
        private readonly IConfigurationProvider configurationProvider;

        protected AutoMapperPagedListHandler(DbContext context, IConfigurationProvider configurationProvider) : base(context)
        {
            this.configurationProvider = configurationProvider;
        }
        
        protected override IQueryable<TResponse> Map(IQueryable<TEntity> entities, TQuery query)
        {
            return entities.ProjectTo<TResponse>(configurationProvider);
        }
    }
}