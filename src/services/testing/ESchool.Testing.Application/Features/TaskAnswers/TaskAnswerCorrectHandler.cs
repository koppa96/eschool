using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Domain.Extensions;
using ESchool.Testing.Domain;
using MediatR;

namespace ESchool.Testing.Application.Features.TaskAnswers
{
    public class TaskAnswerCorrectHandler : IRequestHandler<TaskAnswerCorrectCommand>
    {
        private readonly TestingContext context;

        public TaskAnswerCorrectHandler(TestingContext context)
        {
            this.context = context;
        }
        
        public async Task<Unit> Handle(TaskAnswerCorrectCommand request, CancellationToken cancellationToken)
        {
            var answer = await context.TaskAnswers.FindOrThrowAsync(request.TaskAnswerId, cancellationToken);
            
            answer.ManualCorrect(request.GivenPoints);
            
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}