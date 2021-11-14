using System;
using ESchool.HomeAssignments.Interface.Features.UserHomeworks;
using ESchool.Libs.Interface.Query;

namespace ESchool.HomeAssignments.Interface.Features.Homeworks
{
    public class LessonHomeworkListQuery : PagedListQuery<TeacherHomeworkListResponse>
    {
        public Guid LessonId { get; set; }
    }
}