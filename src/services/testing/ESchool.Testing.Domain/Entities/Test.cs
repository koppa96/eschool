using ESchool.Testing.Domain.Entities.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using ESchool.Testing.Domain.Entities.ClassRegisterData;

namespace ESchool.Testing.Domain.Entities
{
    public class Test
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime ScheduledStart { get; set; }
        public TimeSpan ScheduledLength { get; set; }

        public DateTime? StartedAt { get; private set; }
        public DateTime? ClosedAt { get; private set; }

        public Guid ClassSchoolYearSubjectId { get; set; }
        public virtual ClassSchoolYearSubject ClassSchoolYearSubject { get; set; }

        public virtual ICollection<TestTask> Tasks { get; set; }
        public virtual ICollection<TestAnswer> Answers { get; set; }
        public virtual ICollection<StudentTest> StudentTests { get; set; }

        public void Start()
        {
            if (StartedAt != null)
            {
                throw new InvalidOperationException("A dolgozatírás már el lett indítva.");
            }

            var startTime = DateTime.Now;
            if (startTime < ScheduledStart)
            {
                throw new InvalidOperationException("A dolgozat nem indítható el a tervezett indítási dátum előtt.");
            }

            StartedAt = startTime;
        }

        public void Close()
        {
            if (StartedAt == null)
            {
                throw new InvalidOperationException("A dolgozatot csak a dolgozatírás elindítása után lehet lezárni.");
            }

            if (ClosedAt != null)
            {
                throw new InvalidOperationException("A dolgozat már lezárásra került.");
            }

            var closeTime = DateTime.Now;
            if (StartedAt.Value + ScheduledLength > closeTime)
            {
                throw new InvalidOperationException("A dolgozat nem zárható le idő előtt.");
            }

            ClosedAt = closeTime;

            foreach (var studentTest in StudentTests.Where(x => x.TestAnswer is { Closed: null }))
            {
                studentTest.TestAnswer.Close(true);
            }
        }
    }
}
