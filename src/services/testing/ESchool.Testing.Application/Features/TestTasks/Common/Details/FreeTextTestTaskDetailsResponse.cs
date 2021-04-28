using System.Text.Json.Polymorph.Attributes;

namespace ESchool.Testing.Application.Features.TestTasks.Common.Details
{
    [JsonSubClass(DiscriminatorValue = TestingConstants.Discriminators.FreeText)]
    public class FreeTextTestTaskDetailsResponse : TestTaskDetailsResponse
    {
        
    }
}