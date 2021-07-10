using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.Testing.Application.Features.TestAnswers.Common;
using ESchool.Testing.Domain;
using ESchool.Testing.Interface.Features.TestAnswers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Testing.Application.Features.TestAnswers
{
    public class TestAnswerGetHandler : IRequestHandler<TestAnswerGetQuery, TestAnswerDetailsResponse>
    {
        private readonly TestingContext context;
        private readonly IMapper mapper;

        public TestAnswerGetHandler(TestingContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<TestAnswerDetailsResponse> Handle(TestAnswerGetQuery request, CancellationToken cancellationToken)
        {
            var answer = await context.TestAnswers.Include(x => x.StudentTest)
                    .ThenInclude(x => x.Test)
                        .ThenInclude(x => x.Tasks)
                .Include(x => x.TaskAnswers)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);

            return mapper.Map<TestAnswerDetailsResponse>(answer);
        }
    }
}