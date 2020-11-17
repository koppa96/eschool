using System;
using System.Collections.Generic;

namespace ESchool.Testing.Domain.Entities.ClassRegisterData
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<GroupStudent> GroupStudents { get; set; }

        public virtual ICollection<TestAnswer> TestAnswers { get; set; }
    }
}