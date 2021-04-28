using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.Libs.Domain.Extensions;
using ESchool.Testing.Application.Features.TestTasks.Common.Editor;
using ESchool.Testing.Domain;
using ESchool.Testing.Domain.Entities.Tasks;
using MediatR;

namespace ESchool.Testing.Application.Features.TestTasks
{
    public class TestTaskEditorGetCommand : IRequest<TestTaskEditorResponse>
    {
        public Guid Id { get; set; }
    }
    
    public class TestTaskEditorGetHandler : IRequestHandler<TestTaskEditorGetCommand, TestTaskEditorResponse>
    {
        private readonly TestingContext context;
        private readonly IMapper mapper;

        public TestTaskEditorGetHandler(TestingContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<TestTaskEditorResponse> Handle(TestTaskEditorGetCommand request, CancellationToken cancellationToken)
        {
            var task = await context.Tasks.FindOrThrowAsync(request.Id, cancellationToken);
            return mapper.Map<TestTask, TestTaskEditorResponse>(task);
        }
    }
}