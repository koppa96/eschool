using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.Libs.Domain.Extensions;
using ESchool.Testing.Application.Features.TestTasks.Common.Details;
using ESchool.Testing.Domain;
using ESchool.Testing.Domain.Entities.Tasks;
using MediatR;

namespace ESchool.Testing.Application.Features.TestTasks
{
    public class TestTaskGetCommand : IRequest<TestTaskDetailsResponse>
    {
        public Guid Id { get; set; }
    }
    
    public class TestTaskGetHandler : IRequestHandler<TestTaskGetCommand, TestTaskDetailsResponse>
    {
        private readonly TestingContext context;
        private readonly IMapper mapper;

        public TestTaskGetHandler(TestingContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<TestTaskDetailsResponse> Handle(TestTaskGetCommand request, CancellationToken cancellationToken)
        {
            var task = await context.Tasks.FindOrThrowAsync(request.Id, cancellationToken);
            return mapper.Map<TestTask, TestTaskDetailsResponse>(task);
        }
    }
}