using System;
using System.Collections.Generic;

namespace ESchool.Testing.Domain.Entities.ClassRegisterData
{
    public class ClassSchoolYearSubject
    {
        public Guid Id { get; set; }

        public ClassRegisterEntity Class { get; set; }
        public ClassRegisterEntity SchoolYear { get; set; }
        public ClassRegisterEntity Subject { get; set; }

        public virtual ICollection<Test> Tests { get; set; }
        public virtual ICollection<ClassSchoolYearSubjectStudent> ClassSchoolYearSubjectStudents { get; set; }
        public virtual ICollection<ClassSchoolYearSubjectTeacher> ClassSchoolYearSubjectTeachers { get; set; }
    }
}
