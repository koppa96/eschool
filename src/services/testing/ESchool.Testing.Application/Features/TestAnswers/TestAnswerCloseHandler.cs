using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Domain.Services;
using ESchool.Testing.Domain;
using ESchool.Testing.Interface.Features.TestAnswers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Testing.Application.Features.TestAnswers
{
    public class TestAnswerCloseHandler : IRequestHandler<TestAnswerCloseCommand>
    {
        private readonly TestingContext context;
        private readonly IIdentityService identityService;

        public TestAnswerCloseHandler(TestingContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<Unit> Handle(TestAnswerCloseCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var answer = await context.TestAnswers.Include(x => x.StudentTest)
                .ThenInclude(x => x.Student)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);
            
            if (answer.StudentTest.Student.UserId != currentUserId)
            {
                throw new UnauthorizedAccessException("Mindenki csak a saját dolgozat beadását módosíthatja!");
            }
            
            answer.Close(false);

            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}