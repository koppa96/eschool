using System.Collections.Generic;

namespace ESchool.Testing.Domain.Entities.ClassRegisterData
{
    public class Teacher : UserBase
    {
        public virtual ICollection<GroupTeacher> GroupTeachers { get; set; }
    }
}