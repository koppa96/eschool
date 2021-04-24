using ESchool.Testing.Application.Features.TestAnswers;
using ESchool.Testing.Domain.Attributes;
using ESchool.Testing.Domain.Entities.Tasks.TrueOrFalse;

namespace ESchool.Testing.Application.Features.TaskAnswers.Common
{
    [TaskType(typeof(TrueOrFalseTestTask))]
    public class TrueOrFalseTaskAnswerCreateEditCommand : TaskAnswerCreateEditCommand
    {
        public bool IsTrue { get; set; }
    }
}