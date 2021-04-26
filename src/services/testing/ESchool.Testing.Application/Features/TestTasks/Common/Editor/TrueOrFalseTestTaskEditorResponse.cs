using System.Text.Json.Polymorph.Attributes;

namespace ESchool.Testing.Application.Features.TestTasks.Common.Editor
{
    [JsonSubClass(DiscriminatorValue = TestingConstants.Discriminators.TrueOrFalse)]
    public class TrueOrFalseTestTaskEditorResponse : TestTaskEditorResponse
    {
        public bool IsTrue { get; set; }
    }
}