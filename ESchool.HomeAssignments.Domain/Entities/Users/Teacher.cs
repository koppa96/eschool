using System;
using System.Collections.Generic;
using ESchool.HomeAssignments.Domain.Entities.ClassRegisterData;

namespace ESchool.HomeAssignments.Domain.Entities.Users
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<GroupTeacher> GroupTeachers { get; set; }
    }
}