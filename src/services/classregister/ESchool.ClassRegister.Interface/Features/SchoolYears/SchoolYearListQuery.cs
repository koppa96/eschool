using ESchool.Libs.Interface.Query;

namespace ESchool.ClassRegister.Interface.Features.SchoolYears
{
    public class SchoolYearListQuery : PagedListQuery<SchoolYearListResponse>
    {
        public string Name { get; set; }
        public SchoolYearStatus? Status { get; set; }
    }
}