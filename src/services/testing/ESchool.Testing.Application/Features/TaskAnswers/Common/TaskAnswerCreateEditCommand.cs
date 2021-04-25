using ESchool.Libs.Json.Attributes;

namespace ESchool.Testing.Application.Features.TaskAnswers.Common
{
    [JsonBaseClass(DiscriminatorPropertyName = "taskType")]
    public abstract class TaskAnswerCreateEditCommand
    {
    }
}