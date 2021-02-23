using System;
using ESchool.ClassRegister.Domain.Entities.Users;

namespace ESchool.ClassRegister.Domain.Entities.SubjectManagement
{
    public class ClassSchoolYearSubjectTeacher
    {
        public Guid Id { get; set; }

        public Guid TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

        public Guid ClassSchoolYearSubjectId { get; set; }
        public virtual ClassSchoolYearSubject ClassSchoolYearSubject { get; set; }
    }
}