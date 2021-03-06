﻿using System;
using System.Collections.Generic;

namespace ESchool.HomeAssignments.Domain.Entities.ClassRegisterData
{
    public class Lesson
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public Guid ClassSchoolYearSubjectId { get; set; }
        public virtual ClassSchoolYearSubject ClassSchoolYearSubject { get; set; }
        
        public virtual ICollection<Homework> HomeWorks { get; set; }
    }
}