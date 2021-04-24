using ESchool.Testing.Domain.Attributes;
using ESchool.Testing.Domain.Entities.Tasks.FreeText;

namespace ESchool.Testing.Application.Features.TaskAnswers.Common
{
    [TaskType(typeof(FreeTextTestTask))]
    public class FreeTextTaskAnswerCreateEditCommand : TaskAnswerCreateEditCommand
    {
        public string Answer { get; set; }
    }
}