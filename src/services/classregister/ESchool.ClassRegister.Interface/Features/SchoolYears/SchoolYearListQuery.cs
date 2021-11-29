using System.Collections.Generic;
using ESchool.ClassRegister.SharedDomain.Enums;
using ESchool.Libs.Interface.Query;

namespace ESchool.ClassRegister.Interface.Features.SchoolYears
{
    public class SchoolYearListQuery : OrderedPagedListQuery<SchoolYearListResponse>
    {
        public string Name { get; set; }
        public List<SchoolYearStatus> Statuses { get; set; }
    }
}