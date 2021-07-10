using System.Text.Json.Polymorph.Attributes;

namespace ESchool.Testing.Interface.Features.TestTasks.CreateEdit
{
    [JsonSubClass(DiscriminatorValue = TestingConstants.Discriminators.FreeText)]
    public class FreeTextTestTaskCreateEditCommand : TestTaskCreateEditCommand
    {
        
    }
}