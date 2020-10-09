using ESchool.Testing.Domain.Entities.Answers;
using System;
using System.Collections.Generic;
using System.Text;
using ESchool.Testing.Domain.Entities.ClassRegisterData;

namespace ESchool.Testing.Domain.Entities
{
    public class TestAnswer
    {
        public Guid Id { get; set; }

        public DateTime Started { get; set; }
        public DateTime? Closed { get; set; }
        public bool? ClosedByTeacher { get; set; }

        public Guid TestId { get; set; }
        public virtual Test Test { get; set; }

        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }

        public virtual ICollection<TaskAnswer> TaskAnswers { get; set; }
    }
}
