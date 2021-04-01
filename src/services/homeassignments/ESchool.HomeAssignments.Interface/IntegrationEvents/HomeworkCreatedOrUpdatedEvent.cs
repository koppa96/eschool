using System;

namespace ESchool.HomeAssignments.Interface.IntegrationEvents
{
    public class HomeworkCreatedOrUpdatedEvent
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid LessonId { get; set; }
    }
}