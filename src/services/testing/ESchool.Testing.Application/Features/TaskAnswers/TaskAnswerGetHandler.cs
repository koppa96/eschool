using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.Libs.Domain.Extensions;
using ESchool.Testing.Application.Features.TaskAnswers.Common;
using ESchool.Testing.Domain;
using ESchool.Testing.Domain.Entities.Answers;
using MediatR;

namespace ESchool.Testing.Application.Features.TaskAnswers
{
    public class TaskAnswerGetQuery : IRequest<TaskAnswerResponse>
    {
        public Guid Id { get; set; }
    }
    
    public class TaskAnswerGetHandler : IRequestHandler<TaskAnswerGetQuery, TaskAnswerResponse>
    {
        private readonly TestingContext context;
        private readonly IMapper mapper;

        public TaskAnswerGetHandler(TestingContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<TaskAnswerResponse> Handle(TaskAnswerGetQuery request, CancellationToken cancellationToken)
        {
            var answer = await context.TaskAnswers.FindOrThrowAsync(request.Id, cancellationToken);
            return mapper.Map<TaskAnswer, TaskAnswerResponse>(answer);
        }
    }
}