using System.Text.Json.Polymorph.Attributes;

namespace ESchool.Testing.Application.Features.TaskAnswers.Common
{
    [JsonBaseClass(DiscriminatorName = "taskType")]
    public abstract class TaskAnswerCreateEditCommand
    {
    }
}