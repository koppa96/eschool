using System.Text.Json.Polymorph.Attributes;

namespace ESchool.Testing.Application.Features.TaskAnswers.Common.CreateEdit
{
    [JsonSubClass(DiscriminatorValue = "FreeText")]
    public class FreeTextTaskAnswerCreateEditCommand : TaskAnswerCreateEditCommand
    {
        public string Answer { get; set; }
    }
}