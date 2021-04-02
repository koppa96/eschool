using System.Collections.Generic;
using ESchool.Testing.Domain.Entities.ClassRegisterData;

namespace ESchool.Testing.Domain.Entities.Users
{
    public class Teacher : TestingUserRole
    {
        public virtual ICollection<ClassSchoolYearSubjectTeacher> GroupTeachers { get; set; }
    }
}