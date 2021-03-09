using System;

namespace ESchool.ClassRegister.Interface.IntegrationEvents.Lessons
{
    public class LessonDeletedEvent
    {
        public Guid LessonId { get; set; }
    }
}