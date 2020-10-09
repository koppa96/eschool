using System;
using System.Collections.Generic;

namespace ESchool.Testing.Domain.Entities.ClassRegisterData
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SubjectName { get; set; }

        public virtual ICollection<TestGroup> TestGroups { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
