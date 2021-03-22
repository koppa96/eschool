using ESchool.HomeAssignments.Domain.Entities.ClassRegisterData;
using System.Collections.Generic;

namespace ESchool.HomeAssignments.Domain.Entities.Users
{
    public class Student : UserBase
    {
        public virtual ICollection<StudentHomework> StudentHomeworks { get; set; }
    }
}