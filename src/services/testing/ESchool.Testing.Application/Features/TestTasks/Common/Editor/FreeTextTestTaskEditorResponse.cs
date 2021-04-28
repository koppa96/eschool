using System.Text.Json.Polymorph.Attributes;

namespace ESchool.Testing.Application.Features.TestTasks.Common.Editor
{
    [JsonSubClass(DiscriminatorValue = TestingConstants.Discriminators.FreeText)]
    public class FreeTextTestTaskEditorResponse : TestTaskEditorResponse
    {
    }
}