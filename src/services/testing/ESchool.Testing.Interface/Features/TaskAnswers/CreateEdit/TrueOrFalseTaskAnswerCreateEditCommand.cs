using System.Text.Json.Polymorph.Attributes;

namespace ESchool.Testing.Interface.Features.TaskAnswers.CreateEdit
{
    [JsonSubClass(DiscriminatorValue = TestingConstants.Discriminators.TrueOrFalse)]
    public class TrueOrFalseTaskAnswerCreateEditCommand : TaskAnswerCreateEditCommand
    {
        public bool IsTrue { get; set; }
    }
}