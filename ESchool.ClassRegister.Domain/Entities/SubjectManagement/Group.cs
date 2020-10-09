using ESchool.ClassRegister.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.ClassRegister.Domain.Entities.SubjectManagement
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ClassSubjectGroup> ClassSubjectGroups { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
