using System.Text.Json.Polymorph.Attributes;

namespace ESchool.Testing.Interface.Features.TaskAnswers.CreateEdit
{
    [JsonSubClass(DiscriminatorValue = TestingConstants.Discriminators.FreeText)]
    public class FreeTextTaskAnswerCreateEditCommand : TaskAnswerCreateEditCommand
    {
        public string Answer { get; set; }
    }
}