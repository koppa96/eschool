using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Testing.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Testing.Application.Features.Tests
{
    public class TestCorrectCommand : IRequest
    {
        public Guid TestId { get; set; }
    }
    
    public class TestCorrectHandler : IRequestHandler<TestCorrectCommand>
    {
        private readonly TestingContext context;

        public TestCorrectHandler(TestingContext context)
        {
            this.context = context;
        }
        
        public async Task<Unit> Handle(TestCorrectCommand request, CancellationToken cancellationToken)
        {
            var test = await context.Tests.Include(x => x.Answers)
                    .ThenInclude(x => x.TaskAnswers)
                .SingleAsync(x => x.Id == request.TestId, cancellationToken);

            foreach (var testAnswer in test.Answers)
            {
                foreach (var taskAnswer in testAnswer.TaskAnswers)
                {
                    taskAnswer.AutoCorrect();
                }
            }

            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}