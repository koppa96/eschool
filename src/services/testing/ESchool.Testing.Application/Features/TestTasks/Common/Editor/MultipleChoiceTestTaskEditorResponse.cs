using System;
using System.Collections.Generic;
using System.Text.Json.Polymorph.Attributes;

namespace ESchool.Testing.Application.Features.TestTasks.Common.Editor
{
    [JsonSubClass(DiscriminatorValue = TestingConstants.Discriminators.MultipleChoice)]
    public class MultipleChoiceTestTaskEditorResponse : TestTaskEditorResponse
    {
        public List<MultipleChoiceTestTaskOptionResponse> Options { get; set; }
        public Guid CorrectOptionId { get; set; }
    }
}