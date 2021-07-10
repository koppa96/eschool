using System;
using ESchool.Libs.Interface.Query;

namespace ESchool.HomeAssignments.Interface.Features.UserHomeworks
{
    public class StudentHomeworkListQuery : PagedListQuery<StudentHomeworkListResponse>
    {
        public Guid SchoolYearId { get; set; }
        public Guid SubjectId { get; set; }
        public bool Expired { get; set; }
    }
}