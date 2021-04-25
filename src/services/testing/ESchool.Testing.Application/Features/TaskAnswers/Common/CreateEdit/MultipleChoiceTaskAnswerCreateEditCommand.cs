using System;
using System.Text.Json.Polymorph.Attributes;

namespace ESchool.Testing.Application.Features.TaskAnswers.Common.CreateEdit
{
    [JsonSubClass(DiscriminatorValue = "MultipleChoice")]
    public class MultipleChoiceTaskAnswerCreateEditCommand : TaskAnswerCreateEditCommand
    {
        public Guid SelectedOptionId { get; set; }
    }
}