using System;
using ESchool.Libs.Interface.Query;

namespace ESchool.HomeAssignments.Interface.Features.ClassSchoolYearSubjects
{
    public class SubjectListQuery : PagedListQuery<ClassRegisterItemResponse>
    {
        public Guid SchoolYearId { get; set; }
    }
}