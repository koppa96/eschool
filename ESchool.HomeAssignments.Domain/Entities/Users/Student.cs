using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ESchool.HomeAssignments.Domain.Entities.ClassRegisterData;

namespace ESchool.HomeAssignments.Domain.Entities.Users
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<HomeWorkSolution> Solutions { get; set; }
        public virtual ICollection<GroupStudent> GroupStudents { get; set; }
    }
}