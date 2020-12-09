using System;
using System.Collections.Generic;

namespace ESchool.Testing.Domain.Entities.ClassRegisterData
{
    public class Student : UserBase
    {
        public virtual ICollection<GroupStudent> GroupStudents { get; set; }

        public virtual ICollection<TestAnswer> TestAnswers { get; set; }
    }
}