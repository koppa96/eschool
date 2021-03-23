using ESchool.HomeAssignments.Domain.Entities.ClassRegisterData;
using System.Collections.Generic;

namespace ESchool.HomeAssignments.Domain.Entities.Users
{
    public class Student : HomeAssignmentsUserRole
    {
        public virtual ICollection<StudentHomework> StudentHomeworks { get; set; }
    }
}