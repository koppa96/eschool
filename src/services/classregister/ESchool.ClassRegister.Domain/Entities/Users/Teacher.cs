﻿using System;
using System.Collections.Generic;
using ESchool.ClassRegister.Domain.Attributes;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.ClassRegister.Domain.Entities.Users.Abstractions;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.MultiTenancy.Entities;

namespace ESchool.ClassRegister.Domain.Entities.Users
{
    [TenantUser(TenantRoleType.Teacher)]
    public class Teacher : ClassRegisterUserRole
    {
        public Guid? CurrentClassId { get; set; }
        public virtual Class CurrentClass { get; set; }
        
        public virtual ICollection<Class> PreviousClasses { get; set; }
        public virtual ICollection<ClassSchoolYearSubjectTeacher> ClassSchoolYearSubjectTeachers { get; set; }
        public virtual ICollection<SubjectTeacher> SubjectTeachers { get; set; }
    }
}
