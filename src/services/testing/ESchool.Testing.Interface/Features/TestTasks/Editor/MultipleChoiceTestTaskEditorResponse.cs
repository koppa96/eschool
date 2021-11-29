using System;
using System.Collections.Generic;
using System.Text.Json.Polymorph.Attributes;

namespace ESchool.Testing.Interface.Features.TestTasks.Editor
{
    [JsonSubClass(DiscriminatorValue = TestingConstants.Discriminators.MultipleChoice)]
    public class MultipleChoiceTestTaskEditorResponse : TestTaskEditorResponse
    {
        public List<MultipleChoiceTestTaskOptionResponse> Options { get; set; }
        public Guid CorrectOptionId { get; set; }
    }
}