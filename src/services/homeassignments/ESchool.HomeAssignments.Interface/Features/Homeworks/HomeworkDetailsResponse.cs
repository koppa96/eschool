using System;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.HomeAssignments.Interface.Features.Homeworks
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
        public UserRoleListResponse CreatedBy { get; set; }

        public DateTime? LastModifiedAt { get; set; }
        public UserRoleListResponse LastModifiedBy { get; set; }
        
        public class LessonListResponse
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
        }
    }
}