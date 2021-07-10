using System;

namespace ESchool.ClassRegister.Interface.Features.SubjectManagement.Lessons
{
    public class LessonEditCommand
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
        public bool Canceled { get; set; }
    }
}