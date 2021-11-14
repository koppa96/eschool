using System;
using ESchool.Libs.Interface.Query;

namespace ESchool.HomeAssignments.Interface.Features.ClassSchoolYearSubjects
{
    public class ClassSubjectListQuery : PagedListQuery<ClassSubjectListResponse>
    {
        public Guid SchoolYearId { get; set; }
    }
}