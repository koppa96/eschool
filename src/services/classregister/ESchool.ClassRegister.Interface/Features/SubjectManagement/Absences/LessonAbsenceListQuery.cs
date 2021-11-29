using System;
using ESchool.Libs.Interface.Query;

namespace ESchool.ClassRegister.Interface.Features.SubjectManagement.Absences
{
    public class LessonAbsenceListQuery : PagedListQuery<LessonAbsenceListResponse>
    {
        public Guid LessonId { get; set; }
    }
}