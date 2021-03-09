using System;

namespace ESchool.ClassRegister.Interface.IntegrationEvents.Lessons
{
    public class LessonCreatedOrUpdatedEvent
    {
        public Guid LessonId { get; set; }
    }
}