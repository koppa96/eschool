using System;
using ESchool.Libs.Interface.Query;

namespace ESchool.ClassRegister.Interface.Features.SubjectManagement.Absences
{
    public class AbsenceListQuery : PagedListQuery<AbsenceListResponse>
    {
        public Guid StudentId { get; set; }
        public Guid SchoolYearId { get; set; }
    }
}