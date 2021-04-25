using ESchool.Libs.Json.Attributes;

namespace ESchool.Testing.Application.Features.TaskAnswers.Common
{
    [JsonSubClass(Discriminator = "FreeText")]
    public class FreeTextTaskAnswerCreateEditCommand : TaskAnswerCreateEditCommand
    {
        public string Answer { get; set; }
    }
}