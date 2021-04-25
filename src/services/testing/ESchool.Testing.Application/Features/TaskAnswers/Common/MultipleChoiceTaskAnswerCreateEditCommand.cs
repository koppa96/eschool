using System;
using ESchool.Libs.Json.Attributes;

namespace ESchool.Testing.Application.Features.TaskAnswers.Common
{
    [Discriminator("MultipleChoice")]
    public class MultipleChoiceTaskAnswerCreateEditCommand : TaskAnswerCreateEditCommand
    {
        public Guid SelectedOptionId { get; set; }
    }
}