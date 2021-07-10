using System;
using ESchool.ClassRegister.Interface.Features.Classes;
using ESchool.ClassRegister.Interface.Features.Classrooms;
using ESchool.ClassRegister.Interface.Features.SchoolYears;
using ESchool.ClassRegister.Interface.Features.Subjects;

namespace ESchool.ClassRegister.Interface.Features.SubjectManagement.Lessons
{
    public class LessonDetailsResponse
    {
        public Guid Id { get; set; }
        public bool Canceled { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }

        public SchoolYearListResponse SchoolYear { get; set; }
        public ClassListResponse Class { get; set; }
        public SubjectListResponse Subject { get; set; }
        public ClassroomListResponse Classroom { get; set; }
    }
}