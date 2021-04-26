using System;
using System.Text.Json.Polymorph.Attributes;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.Testing.Application.Features.TestTasks.Common.Editor;
using ESchool.Testing.Domain;
using ESchool.Testing.Domain.Entities.Tasks;
using MediatR;

namespace ESchool.Testing.Application.Features.TestTasks.Create
{
    [JsonBaseClass(DiscriminatorName = TestingConstants.DiscriminatorName)]
    public abstract class TestTaskCreateEditCommand : IRequest<TestTaskEditorResponse>
    {
        public Guid TestId { get; set; }
        
        public string Description { get; set; }

        public int PointValue { get; set; }
        public int IncorrectAnswerPointValue { get; set; }
    }
    
    public abstract class TestTaskCreateHandler<TCommand> : IRequestHandler<TCommand, TestTaskEditorResponse>
        where TCommand : TestTaskCreateEditCommand
    {
        private readonly TestingContext context;
        private readonly IMapper mapper;

        protected TestTaskCreateHandler(TestingContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<TestTaskEditorResponse> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var task = mapper.Map<TestTaskCreateEditCommand, TestTask>(request);
            context.Tasks.Add(task);
            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<TestTaskEditorResponse>(task);
        }
    }
}