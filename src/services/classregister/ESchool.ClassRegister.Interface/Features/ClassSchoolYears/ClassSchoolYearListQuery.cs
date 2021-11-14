using System;
using ESchool.ClassRegister.Interface.Features.Classes;
using ESchool.Libs.Interface.Query;

namespace ESchool.ClassRegister.Interface.Features.ClassSchoolYears
{
    public class ClassSchoolYearListQuery : PagedListQuery<ClassListResponse>
    {
        public Guid SchoolYearId { get; set; }
    }
}