using System.Collections.Generic;
using ESchool.Libs.Interface.Enums;

namespace ESchool.Libs.Interface.Query
{
    public class OrderedPagedListQuery<TResponse> : PagedListQuery<TResponse>
    {
        public Dictionary<string, OrderingDirection> Orderings { get; set; }
    }
}