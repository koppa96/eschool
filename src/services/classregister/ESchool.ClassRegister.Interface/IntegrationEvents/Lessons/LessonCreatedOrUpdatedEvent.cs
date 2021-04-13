using System;

namespace ESchool.ClassRegister.Interface.IntegrationEvents.Lessons
{
    public class LessonCreatedOrUpdatedEvent
    {
        public Guid LessonId { get; set; }
        public string Title { get; set; }
        public Guid ClassSchoolYearSubjectId { get; set; }
    }
}