using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.Libs.Domain.Extensions;
using ESchool.Testing.Domain;
using ESchool.Testing.Domain.Entities.Tasks;
using ESchool.Testing.Interface.Features.TestTasks;
using ESchool.Testing.Interface.Features.TestTasks.Details;
using MediatR;

namespace ESchool.Testing.Application.Features.TestTasks
{
    public class TestTaskGetHandler : IRequestHandler<TestTaskGetQuery, TestTaskDetailsResponse>
    {
        private readonly TestingContext context;
        private readonly IMapper mapper;

        public TestTaskGetHandler(TestingContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<TestTaskDetailsResponse> Handle(TestTaskGetQuery request, CancellationToken cancellationToken)
        {
            var task = await context.Tasks.FindOrThrowAsync(request.Id, cancellationToken);
            return mapper.Map<TestTask, TestTaskDetailsResponse>(task);
        }
    }
}