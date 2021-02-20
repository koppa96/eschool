﻿using ESchool.ClassRegister.Domain.Entities.Grading;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using System;
using System.Collections.Generic;
using ESchool.ClassRegister.Domain.Attributes;
using ESchool.Libs.Domain.Enums;

namespace ESchool.ClassRegister.Domain.Entities.Users
{
    [TenantUser(TenantRoleType.Student)]
    public class Student : UserBase
    {
        public string StudentIdentificationNumber { get; set; }

        public Guid ClassId { get; set; }
        public virtual Class Class { get; set; }

        public virtual ICollection<Grade> Grades { get; set; }
        public virtual ICollection<SmallGrade> SmallGrades { get; set; }
        public virtual ICollection<Absence> Absences { get; set; }
        public virtual ICollection<StudentParent> StudentParents { get; set; }
        public virtual ICollection<GroupStudent> GroupStudents { get; set; }
    }
}