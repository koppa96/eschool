using System;
using ESchool.ClassRegister.Interface.Features.Classrooms;
using ESchool.ClassRegister.Interface.Features.Subjects;

namespace ESchool.ClassRegister.Interface.Features.SubjectManagement.Lessons
{
    public class LessonListResponse
    {
        public Guid Id { get; set; }
        public SubjectListResponse Subject { get; set; }
        public ClassroomListResponse Classroom { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
        public bool Canceled { get; set; }
    }
}