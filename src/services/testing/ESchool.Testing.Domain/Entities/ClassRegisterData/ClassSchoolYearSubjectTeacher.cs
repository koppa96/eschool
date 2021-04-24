using System;
using ESchool.Testing.Domain.Entities.Users;

namespace ESchool.Testing.Domain.Entities.ClassRegisterData
{
    public class ClassSchoolYearSubjectTeacher
    {
        public Guid Id { get; set; }

        public Guid ClassSchoolYearSubjectId { get; set; }
        public virtual ClassSchoolYearSubject ClassSchoolYearSubject { get; set; }

        public Guid TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}