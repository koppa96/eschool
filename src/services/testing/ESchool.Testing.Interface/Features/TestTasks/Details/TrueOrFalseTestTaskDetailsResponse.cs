using System.Text.Json.Polymorph.Attributes;

namespace ESchool.Testing.Interface.Features.TestTasks.Details
{
    [JsonSubClass(DiscriminatorValue = TestingConstants.Discriminators.TrueOrFalse)]
    public class TrueOrFalseTestTaskDetailsResponse : TestTaskDetailsResponse
    {
        
    }
}