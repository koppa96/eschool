using System;
using System.Collections.Generic;

namespace ESchool.Testing.Application.Features.TestAnswers.Common
{
    public class TestAnswerDetailsResponse
    {
        public Guid Id { get; set; }
        
        public DateTime Started { get; set; }
        public DateTime? Closed { get; set; }

        public List<TaskAnswerStatus> TaskAnswers { get; set; }
        
        public class TaskAnswerStatus
        {
            public Guid TaskId { get; set; }
            public Guid? TaskAnswerId { get; set; }
        }
    }
}