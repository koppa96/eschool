﻿using System;
using System.Collections.Generic;

namespace ESchool.ClassRegister.Domain.Entities.SubjectManagement
{
    public class ClassSubject
    {
        public Guid Id { get; set; }

        public Guid ClassSchoolYearId { get; set; }
        public virtual ClassSchoolYear ClassSchoolYear { get; set; }
        
        public Guid SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        public virtual ICollection<ClassSubjectGroup> ClassSubjectGroups { get; set; }
    }
}
