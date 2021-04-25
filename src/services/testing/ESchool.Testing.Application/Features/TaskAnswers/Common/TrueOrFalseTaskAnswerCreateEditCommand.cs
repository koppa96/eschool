using ESchool.Libs.Json.Attributes;

namespace ESchool.Testing.Application.Features.TaskAnswers.Common
{
    [Discriminator("TrueOrFalse")]
    public class TrueOrFalseTaskAnswerCreateEditCommand : TaskAnswerCreateEditCommand
    {
        public bool IsTrue { get; set; }
    }
}