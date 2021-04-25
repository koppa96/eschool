using System;
using ESchool.Testing.Application.Features.TaskAnswers.Common.CreateEdit;

namespace ESchool.Testing.Application.Features.TaskAnswers
{
    public class TaskAnswerCreateCommand
    {
        public Guid TaskId { get; set; }
        public TaskAnswerCreateEditCommand TaskAnswer { get; set; }
    }
    
    public class TaskAnswerCreateHandler
    {
        
    }
}