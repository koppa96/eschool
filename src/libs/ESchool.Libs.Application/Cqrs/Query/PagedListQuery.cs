using System;
using ESchool.Libs.Application.Cqrs.Response;
using MediatR;

namespace ESchool.Libs.Application.Cqrs.Query
{
    public class PagedListQuery
    {
        public const int DefaultPageSize = 25;
        
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        
        public PagedListQuery()
        {
            PageSize = DefaultPageSize;
        }

        public TQuery ToTypedQuery<TQuery>(Action<TQuery> initialize)
            where TQuery : PagedListQuery, new()
        {
            var query = new TQuery
            {
                PageIndex = PageIndex,
                PageSize = PageSize
            };
            initialize(query);
            return query;
        }
    }
    
    public class PagedListQuery<TResponse> : PagedListQuery, IRequest<PagedListResponse<TResponse>>
    {
        
    }
}