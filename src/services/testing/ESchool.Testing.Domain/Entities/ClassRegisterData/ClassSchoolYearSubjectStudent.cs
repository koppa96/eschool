using System;
using ESchool.Testing.Domain.Entities.Users;

namespace ESchool.Testing.Domain.Entities.ClassRegisterData
{
    public class ClassSchoolYearSubjectStudent
    {
        public Guid Id { get; set; }

        public Guid ClassSchoolYearSubjectId { get; set; }
        public virtual ClassSchoolYearSubject ClassSchoolYearSubject { get; set; }

        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}