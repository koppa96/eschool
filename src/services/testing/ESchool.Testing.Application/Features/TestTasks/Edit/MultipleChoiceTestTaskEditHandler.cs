using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Testing.Application.Features.TestTasks.Common.Editor;
using ESchool.Testing.Application.Features.TestTasks.Create;
using ESchool.Testing.Domain;
using ESchool.Testing.Domain.Entities.Tasks.MultipleChoice;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Testing.Application.Features.TestTasks.Edit
{
    public class MultipleChoiceTestTaskEditHandler : IRequestHandler<EditCommand<MultipleChoiceTestTaskCreateEditCommand, TestTaskEditorResponse>, TestTaskEditorResponse>
    {
        private readonly TestingContext context;
        private readonly IMapper mapper;

        public MultipleChoiceTestTaskEditHandler(TestingContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<TestTaskEditorResponse> Handle(
            EditCommand<MultipleChoiceTestTaskCreateEditCommand, TestTaskEditorResponse> request,
            CancellationToken cancellationToken)
        {
            var task = await context.Set<MultipleChoiceTestTask>()
                .Include(x => x.Options)
                .Include(x => x.Test)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);

            if (task.Test.StartedAt != null)
            {
                throw new InvalidOperationException("A feladatok nem szerkeszthetők a dolgozat megkezése után.");
            }
            
            context.Set<MultipleChoiceTestTaskOption>()
                .RemoveRange(task.Options);

            var options = request.InnerCommand.Options.Select(x => new MultipleChoiceTestTaskOption
            {
                Id = Guid.NewGuid(),
                Value = x,
                TestTask = task
            }).ToList();
            
            context.Set<MultipleChoiceTestTaskOption>()
                .AddRange(options);

            task.CorrectOption = options[request.InnerCommand.CorrectOptionIndex];

            task.Description = request.InnerCommand.Description;
            task.PointValue = request.InnerCommand.PointValue;
            task.IncorrectAnswerPointValue = request.InnerCommand.IncorrectAnswerPointValue;

            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<MultipleChoiceTestTaskEditorResponse>(task);
        }
    }
}