using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Testing.Domain;
using ESchool.Testing.Domain.Entities.Answers;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Testing.Application.Features.TaskAnswers
{
    public class TaskAnswerDeleteCommand : DeleteCommand
    {
    }
    
    public class TaskAnswerDeleteHandler : DeleteHandler<TaskAnswerDeleteCommand, TaskAnswer>
    {
        public TaskAnswerDeleteHandler(TestingContext context) : base(context)
        {
        }
    }
}