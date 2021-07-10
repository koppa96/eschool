using System;
using System.Collections.Generic;

namespace ESchool.Testing.Interface.Features.Tests
{
    public class TestEditCommand
    {
        public string Name { get; set; }
        
        public DateTime ScheduledStart { get; set; }
        public int ScheduledLengthInMinutes { get; set; }
        public List<Guid> StudentIds { get; set; }
    }
}