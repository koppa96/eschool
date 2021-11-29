using System;
using ESchool.Libs.Interface.Query;

namespace ESchool.HomeAssignments.Interface.Features.UserHomeworks
{
    public class TeacherHomeworkListQuery : PagedListQuery<TeacherHomeworkListResponse>
    {
        public Guid ClassId { get; set; }
        public Guid SchoolYearId { get; set; }
        public Guid SubjectId { get; set; }
        public bool IncludeReviewed { get; set; }
    }
}