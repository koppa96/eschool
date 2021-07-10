using System;

namespace ESchool.HomeAssignments.Interface.Features.UserHomeworks
{
    public class TeacherHomeworkListResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Deadline { get; set; }
        public bool Optional { get; set; }
        public int Submissions { get; set; }
        public int Reviews { get; set; }
    }
}