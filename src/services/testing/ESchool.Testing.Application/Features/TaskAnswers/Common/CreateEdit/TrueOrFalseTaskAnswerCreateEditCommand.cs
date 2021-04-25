using System.Text.Json.Polymorph.Attributes;

namespace ESchool.Testing.Application.Features.TaskAnswers.Common.CreateEdit
{
    [JsonSubClass(DiscriminatorValue = "TrueOrFalse")]
    public class TrueOrFalseTaskAnswerCreateEditCommand : TaskAnswerCreateEditCommand
    {
        public bool IsTrue { get; set; }
    }
}