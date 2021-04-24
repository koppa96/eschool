using System;
using ESchool.Testing.Domain.Attributes;
using ESchool.Testing.Domain.Entities.Tasks.MultipleChoice;

namespace ESchool.Testing.Application.Features.TaskAnswers.Common
{
    [TaskType(typeof(MultipleChoiceTestTask))]
    public class MultipleChoiceTaskAnswerCreateEditCommand : TaskAnswerCreateEditCommand
    {
        public Guid SelectedOptionId { get; set; }
    }
}