namespace ESchool.Libs.Application.Cqrs.Query
{
    public class PagedListQuery
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}