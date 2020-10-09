using ESchool.Testing.Domain.Entities.Tasks;
using ESchool.Testing.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.Testing.Domain.Entities
{
    public class Test
    {
        public Guid Id { get; set; }

        public DateTime ScheduledStart { get; set; }
        public TimeSpan ScheduledLength { get; set; }

        public DateTime? StartedAt { get; set; }
        public DateTime? ClosedAt { get; set; }

        public virtual ICollection<TestTask> Tasks { get; set; }
        public virtual ICollection<TestGroup> TestGroups { get; set; }
        public virtual ICollection<TestAnswer> Answers { get; set; }

    }
}
