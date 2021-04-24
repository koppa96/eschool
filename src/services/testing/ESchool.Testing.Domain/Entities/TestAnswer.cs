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
        public DateTime? Closed { get; private set; }
        public bool? ClosedByTeacher { get; private set; }
        
        public virtual StudentTest StudentTest { get; set; }

        public virtual ICollection<TaskAnswer> TaskAnswers { get; set; }

        public void Close(bool closedByTeacher)
        {
            if (Closed != null)
            {
                throw new InvalidOperationException("A dolgozatbeadás már le van zárva.");
            }
            
            Closed = DateTime.Now;
            ClosedByTeacher = closedByTeacher;
        }
    }
}
