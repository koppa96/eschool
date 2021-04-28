using System;
using System.Text.Json.Polymorph.Attributes;

namespace ESchool.Testing.Application.Features.TestTasks.Common.Details
{
    [JsonBaseClass(DiscriminatorName = TestingConstants.DiscriminatorName)]
    public class TestTaskDetailsResponse
    {
        public Guid Id { get; set; }
        
        public string Description { get; set; }

        public int PointValue { get; set; }
        public int IncorrectAnswerPointValue { get; set; }
    }
}