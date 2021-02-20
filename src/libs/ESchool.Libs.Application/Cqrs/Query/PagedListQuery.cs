using ESchool.Libs.Application.Cqrs.Response;
using MediatR;

namespace ESchool.Libs.Application.Cqrs.Query
{
    public class PagedListQuery<TResponse> : IRequest<PagedListResponse<TResponse>>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public PagedListQuery()
        {
            PageSize = 25;
        }
    }
}