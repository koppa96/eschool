using System;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.Lessons;
using ESchool.ClassRegister.SharedDomain.Enums;

namespace ESchool.ClassRegister.Interface.Features.SubjectManagement.Absences
{
    public class AbsenceListResponse
    {
        public Guid Id { get; set; }
        public AbsenceState AbsenceState { get; set; }
        public LessonListResponse Lesson { get; set; }
    }
}