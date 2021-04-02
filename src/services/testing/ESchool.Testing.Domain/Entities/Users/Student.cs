using System.Collections.Generic;
using ESchool.Testing.Domain.Entities.ClassRegisterData;

namespace ESchool.Testing.Domain.Entities.Users
{
    public class Student : TestingUserRole
    {
        public virtual ICollection<ClassSchoolYearSubjectStudent> ClassSchoolYearSubjectStudents { get; set; }

        public virtual ICollection<TestAnswer> TestAnswers { get; set; }
    }
}