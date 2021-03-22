using System;

namespace ESchool.HomeAssignments.Application.Features.Homeworks.Common
{
    public class HomeworkDetailsResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public bool Optional { get; set; }
        public LessonListResponse Lesson { get; set; }
        
        public class LessonListResponse
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
        }
    }
}