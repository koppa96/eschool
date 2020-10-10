using ESchool.Libs.Domain.Interfaces;
using System;
using System.Collections.Generic;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.ClassRegister.Domain.Entities.Users;

namespace ESchool.ClassRegister.Domain.Entities
{
    public class Class : IMultiTenantEntity
    {
        public Guid Id { get; set; }
        
        public Guid TenantId { get; set; }

        public Guid? HeadTeacherId { get; set; }
        public virtual Teacher HeadTeacher { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<ClassSubject> ClassSubjects { get; set; }
    }
}
