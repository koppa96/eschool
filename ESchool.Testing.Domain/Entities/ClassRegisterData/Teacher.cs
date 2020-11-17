using System;
using System.Collections.Generic;

namespace ESchool.Testing.Domain.Entities.ClassRegisterData
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<GroupTeacher> GroupTeachers { get; set; }
    }
}