using System;

namespace ESchool.ClassRegister.Interface.Features.SubjectManagement.Lessons
{
    public class LessonInfoResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid ClassSchoolYearSubjectId { get; set; }
    }
}