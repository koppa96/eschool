using System;
using System.Text.Json.Polymorph.Attributes;

namespace ESchool.Testing.Interface.Features.TestTasks.Editor
{
    [JsonBaseClass(DiscriminatorName = TestingConstants.DiscriminatorName)]
    public abstract class TestTaskEditorResponse
    {
        public Guid Id { get; set; }
        
        public string Description { get; set; }

        public int PointValue { get; set; }
        public int IncorrectAnswerPointValue { get; set; }
    }
}