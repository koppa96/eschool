using System.Collections.Generic;
using System.Text.Json.Polymorph.Attributes;

namespace ESchool.Testing.Application.Features.TestTasks.Common.Details
{
    [JsonSubClass(DiscriminatorValue = TestingConstants.Discriminators.MultipleChoice)]
    public class MultipleChoiceTestTaskDetailsResponse : TestTaskDetailsResponse
    {
        public List<MultipleChoiceTestTaskOptionResponse> Options { get; set; }
        
        
    }
}