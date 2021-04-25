using ESchool.Libs.Json.Attributes;

namespace ESchool.Testing.Application.Features.TaskAnswers.Common
{
    [JsonSubClass(Discriminator = "TrueOrFalse")]
    public class TrueOrFalseTaskAnswerCreateEditCommand : TaskAnswerCreateEditCommand
    {
        public bool IsTrue { get; set; }
    }
}