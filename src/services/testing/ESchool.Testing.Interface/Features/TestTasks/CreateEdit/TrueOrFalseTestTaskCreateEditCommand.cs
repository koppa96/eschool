using System.Text.Json.Polymorph.Attributes;

namespace ESchool.Testing.Interface.Features.TestTasks.CreateEdit
{
    [JsonSubClass(DiscriminatorValue = TestingConstants.Discriminators.TrueOrFalse)]
    public class TrueOrFalseTestTaskCreateEditCommand : TestTaskCreateEditCommand
    {
        public bool IsTrue { get; set; }
    }
}