using System;

namespace ESchool.Testing.Interface.Features.Tests
{
    public class TestDetailsResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public DateTime ScheduledStart { get; set; }
        public TimeSpan ScheduledLength { get; set; }

        public DateTime? StartedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
    }
}