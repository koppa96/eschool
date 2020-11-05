using System;
using System.Collections.Generic;
using ESchool.ClassRegister.Domain.Attributes;
using ESchool.ClassRegister.Domain.Entities.Grading;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.Libs.Domain.Enums;

namespace ESchool.ClassRegister.Domain.Entities.Users
{
    [TenantUser(TenantRoleType.Teacher)]
    public class Teacher : UserBase
    {
        public virtual ICollection<GroupTeacher> GroupTeachers { get; set; }

        public Guid? CurrentClassId { get; set; }
        public virtual Class CurrentClass { get; set; }

        public virtual ICollection<Class> PreviousClasses { get; set; }
    }
}
