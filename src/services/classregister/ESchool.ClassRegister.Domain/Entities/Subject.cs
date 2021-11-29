using ESchool.Libs.Domain.Interfaces;
using System;
using System.Collections.Generic;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;

namespace ESchool.ClassRegister.Domain.Entities
{
    public class Subject : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<SubjectTeacher> SubjectTeachers { get; set; }
        public virtual ICollection<ClassSchoolYearSubject> ClassSchoolYearSubjects { get; set; }
    }
}
