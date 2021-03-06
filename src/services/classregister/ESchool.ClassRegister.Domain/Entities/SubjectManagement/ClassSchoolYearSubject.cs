﻿using System;
using System.Collections.Generic;
using ESchool.ClassRegister.Domain.Entities.Grading;

namespace ESchool.ClassRegister.Domain.Entities.SubjectManagement
{
    public class ClassSchoolYearSubject
    {
        public Guid Id { get; set; }

        public Guid ClassSchoolYearId { get; set; }
        public virtual ClassSchoolYear ClassSchoolYear { get; set; }
        
        public Guid SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
        
        public virtual ICollection<ClassSchoolYearSubjectTeacher> ClassSchoolYearSubjectTeachers { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
    }
}
