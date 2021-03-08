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
    }
    
    public class PagedListQuery<TResponse> : PagedListQuery, IRequest<PagedListResponse<TResponse>>
    {
        
    }
}