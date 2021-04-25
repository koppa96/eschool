using ESchool.Libs.Json.Attributes;

namespace ESchool.Testing.Application.Features.TaskAnswers.Common
{
    [Discriminator("FreeText")]
    public class FreeTextTaskAnswerCreateEditCommand : TaskAnswerCreateEditCommand
    {
        public string Answer { get; set; }
    }
}