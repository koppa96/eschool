using System.Text.Json.Polymorph.Attributes;

namespace ESchool.Testing.Application.Features.TaskAnswers.Common.CreateEdit
{
    [JsonBaseClass(DiscriminatorName = "taskType")]
    public abstract class TaskAnswerCreateEditCommand
    {
    }
}