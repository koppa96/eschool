using ESchool.ClassRegister.Domain.Entities.Grading;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.Libs.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.ClassRegister.Domain.Entities.Users
{
    public class Student : UserBase, IMultiTenantEntity
    {
        public string StudentIdentificationNumber { get; set; }

        public Guid ClassId { get; set; }
        public virtual Class Class { get; set; }

        public virtual ICollection<Grade> Grades { get; set; }
        public virtual ICollection<Absence> Absences { get; set; }
        public virtual ICollection<StudentParent> StudentParents { get; set; }

        public Guid TenantId { get; set; }
    }
}
