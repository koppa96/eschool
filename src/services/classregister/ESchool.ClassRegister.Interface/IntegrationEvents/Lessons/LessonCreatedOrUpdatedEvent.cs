using System;

namespace ESchool.ClassRegister.Interface.IntegrationEvents.Lessons
{
    public class LessonCreatedOrUpdatedEvent
    {
        public Guid LessonId { get; set; }
        public string Title { get; set; }
        public Guid ClassId { get; set; }
        public Guid SchoolYearId { get; set; }
        public Guid SubjectId { get; set; }
    }
}