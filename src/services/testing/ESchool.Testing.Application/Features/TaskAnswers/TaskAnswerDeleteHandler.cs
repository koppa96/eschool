using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Testing.Domain;
using ESchool.Testing.Domain.Entities.Answers;

namespace ESchool.Testing.Application.Features.TaskAnswers
{
    public class TaskAnswerDeleteHandler : DeleteHandler<TaskAnswerDeleteCommand, TaskAnswer>
    {
        public TaskAnswerDeleteHandler(TestingContext context) : base(context)
        {
        }
    }
}