using System;

namespace ESchool.Libs.Application.Cqrs.Response
{
    public class DropdownResponse<TKey>
    {
        public TKey Id { get; set; }
        public string Value { get; set; }
    }

    public class DropdownResponse : DropdownResponse<Guid>
    {
    }
}