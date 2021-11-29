using System;
using ESchool.Libs.Interface.Query;

namespace ESchool.HomeAssignments.Interface.Features.Lessons
{
    public class LessonListQuery : PagedListQuery<HomeAssignmentsLessonListResponse>
    {
        public Guid SchoolYearId { get; set; }
        public Guid ClassId { get; set; }
        public Guid SubjectId { get; set; }
    }
}