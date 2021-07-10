using System;
using System.Collections.Generic;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.Testing.Interface.Features.TestAnswers
{
    public class TestAnswerDetailsResponse
    {
        public Guid Id { get; set; }

        public UserRoleListResponse Student { get; set; }
        
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