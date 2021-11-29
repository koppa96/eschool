using System;
using System.Collections.Generic;
using MediatR;

namespace ESchool.Testing.Interface.Features.Tests
{
    public class TestCreateCommand : IRequest<TestDetailsResponse>
    {
        public string Name { get; set; }
        
        public DateTime ScheduledStart { get; set; }
        public int ScheduledLengthInMinutes { get; set; }
        public List<Guid> StudentIds { get; set; }

        public Guid ClassSchoolYearSubjectId { get; set; }
    }
}