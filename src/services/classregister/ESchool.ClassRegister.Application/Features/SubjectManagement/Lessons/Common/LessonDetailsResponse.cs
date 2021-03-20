using System;
using ESchool.ClassRegister.Application.Features.Classes.Common;
using ESchool.ClassRegister.Application.Features.Classrooms;
using ESchool.ClassRegister.Application.Features.SchoolYears;
using ESchool.ClassRegister.Application.Features.Subjects;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons.Common
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