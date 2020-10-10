using System;

namespace ESchool.HomeAssignments.Domain.Entities
{
    public class HomeWork
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public bool Optional { get; set; }

        public Guid LessonId { get; set; }
    }
}
