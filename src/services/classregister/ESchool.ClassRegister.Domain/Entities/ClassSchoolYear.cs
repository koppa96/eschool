using System;
using System.Collections.Generic;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;

namespace ESchool.ClassRegister.Domain.Entities
{
    public class ClassSchoolYear
    {
        public Guid Id { get; set; }

        public Guid SchoolYearId { get; set; }
        public virtual SchoolYear SchoolYear { get; set; }

        public Guid ClassId { get; set; }
        public virtual Class Class { get; set; }

        public virtual ICollection<ClassSchoolYearSubject> ClassSchoolYearSubjects { get; set; }
    }
}