﻿using System;
using System.Collections.Generic;
using ESchool.ClassRegister.Domain.Entities.Grading;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;

namespace ESchool.ClassRegister.Domain.Entities.Users
{
    public class Teacher : UserBase
    {
        public virtual ICollection<GroupTeacher> GroupTeachers { get; set; }

        public Guid? CurrentClassId { get; set; }
        public virtual Class CurrentClass { get; set; }

        public virtual ICollection<Class> PreviousClasses { get; set; }
    }
}
