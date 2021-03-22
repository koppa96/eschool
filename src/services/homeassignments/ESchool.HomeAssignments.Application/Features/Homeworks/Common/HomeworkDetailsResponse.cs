using System;
using ESchool.HomeAssignments.Application.Features.Users.Common;

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

        public DateTime CreatedAt { get; set; }
        public UserListResponse CreatedBy { get; set; }

        public DateTime? LastModifiedAt { get; set; }
        public UserListResponse LastModifiedBy { get; set; }
        
        public class LessonListResponse
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
        }
    }
}