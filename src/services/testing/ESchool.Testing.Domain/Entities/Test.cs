using ESchool.Testing.Domain.Entities.Tasks;
using System;
using System.Collections.Generic;
using ESchool.Testing.Domain.Entities.ClassRegisterData;

namespace ESchool.Testing.Domain.Entities
{
    public class Test
    {
        public Guid Id { get; set; }

        public DateTime ScheduledStart { get; set; }
        public TimeSpan ScheduledLength { get; set; }

        public DateTime? StartedAt { get; set; }
        public DateTime? ClosedAt { get; set; }

        public Guid ClassSchoolYearSubjectId { get; set; }
        public virtual ClassSchoolYearSubject ClassSchoolYearSubject { get; set; }

        public virtual ICollection<TestTask> Tasks { get; set; }
        public virtual ICollection<TestAnswer> Answers { get; set; }
        public virtual ICollection<StudentTest> StudentTests { get; set; }
    }
}
