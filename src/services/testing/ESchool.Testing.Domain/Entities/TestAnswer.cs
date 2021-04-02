using ESchool.Testing.Domain.Entities.Answers;
using System;
using System.Collections.Generic;
using ESchool.Testing.Domain.Entities.ClassRegisterData;
using ESchool.Testing.Domain.Entities.Users;

namespace ESchool.Testing.Domain.Entities
{
    public class TestAnswer
    {
        public Guid Id { get; set; }

        public DateTime Started { get; set; }
        public DateTime? Closed { get; set; }
        public bool? ClosedByTeacher { get; set; }
        
        public virtual StudentTest StudentTest { get; set; }

        public virtual ICollection<TaskAnswer> TaskAnswers { get; set; }
    }
}
