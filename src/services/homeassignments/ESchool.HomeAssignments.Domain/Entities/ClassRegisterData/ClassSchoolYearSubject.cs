using System;
using System.Collections.Generic;

namespace ESchool.HomeAssignments.Domain.Entities.ClassRegisterData
{
    public class ClassSchoolYearSubject
    {
        public Guid Id { get; set; }

        public Guid ClassId { get; set; }
        public Guid SchoolYearId { get; set; }
        public Guid SubjectId { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<ClassSchoolYearSubjectStudent> ClassSchoolYearSubjectStudents { get; set; }
        public virtual ICollection<ClassSchoolYearSubjectTeacher> ClassSchoolYearSubjectTeachers { get; set; }
    }
}