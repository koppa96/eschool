using System;
using ESchool.Libs.Interface.Query;

namespace ESchool.ClassRegister.Interface.Features.SubjectManagement.Lessons
{
    public class AdminLessonListQuery : PagedListQuery<LessonListResponse>
    {
        public Guid ClassId { get; set; }
        public Guid SchoolYearId { get; set; }
        public Guid SubjectId { get; set; }
    }
}