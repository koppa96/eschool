using System;
using System.Collections.Generic;
using ESchool.HomeAssignments.Domain.Entities.ClassRegisterData;

namespace ESchool.HomeAssignments.Domain.Entities.Users
{
    public class Teacher : UserBase
    {
        public virtual ICollection<GroupTeacher> GroupTeachers { get; set; }
    }
}