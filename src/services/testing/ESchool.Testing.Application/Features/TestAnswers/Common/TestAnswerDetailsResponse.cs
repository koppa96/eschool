using System;
using System.Collections.Generic;
using ESchool.ClassRegister.Grpc;

namespace ESchool.Testing.Application.Features.TestAnswers.Common
{
    public class TestAnswerDetailsResponse
    {
        public Guid Id { get; set; }

        public UserListResponse Student { get; set; }
        
        public DateTime Started { get; set; }
        public DateTime? Closed { get; set; }

        public int Points { get; set; }
        public bool HasBeenCorrected { get; set; }
        
        public List<TaskStatus> Tasks { get; set; }
        
        public class TaskStatus
        {
            public Guid TaskId { get; set; }
            public Guid? TaskAnswerId { get; set; }
        }
    }
}