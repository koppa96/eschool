using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.Testing.Application.Features.TestTasks.Common;
using ESchool.Testing.Domain;
using ESchool.Testing.Domain.Entities.Tasks.MultipleChoice;
using ESchool.Testing.Interface.Features.TestTasks.CreateEdit;
using ESchool.Testing.Interface.Features.TestTasks.Editor;
using MediatR;

namespace ESchool.Testing.Application.Features.TestTasks.Create
{
    public class MultipleChoiceTestTaskCreateHandler : IRequestHandler<MultipleChoiceTestTaskCreateEditCommand, TestTaskEditorResponse>
    {
        private readonly TestingContext context;
        private readonly IMapper mapper;

        public MultipleChoiceTestTaskCreateHandler(TestingContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<TestTaskEditorResponse> Handle(MultipleChoiceTestTaskCreateEditCommand request, CancellationToken cancellationToken)
        {
            var options = request.Options.Select(x => new MultipleChoiceTestTaskOption
            {
                Value = x
            }).ToList();

            var task = new MultipleChoiceTestTask
            {
                Description = request.Description,
                CorrectOption = options[request.CorrectOptionIndex],
                Options = options,
                IncorrectAnswerPointValue = request.IncorrectAnswerPointValue,
                PointValue = request.PointValue,
                TestId = request.TestId
            };

            context.Tasks.Add(task);
            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<MultipleChoiceTestTaskEditorResponse>(task);
        }
    }
}