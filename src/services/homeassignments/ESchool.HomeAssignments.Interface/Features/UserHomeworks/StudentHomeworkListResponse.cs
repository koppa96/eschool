using System;

namespace ESchool.HomeAssignments.Interface.Features.UserHomeworks
{
    public class StudentHomeworkListResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Deadline { get; set; }
        public bool Submitted { get; set; }
        public bool Optional { get; set; }
    }
}