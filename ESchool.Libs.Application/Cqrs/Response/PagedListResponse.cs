﻿using System.Collections.Generic;

namespace ESchool.Libs.Application.Cqrs.Response
{
    public class PagedListResponse<TItem>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<TItem> Items { get; set; }
    }
}