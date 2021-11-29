using System.Collections.Generic;
using System.Text.Json.Polymorph.Attributes;

namespace ESchool.Testing.Interface.Features.TestTasks.CreateEdit
{
    [JsonSubClass(DiscriminatorValue = TestingConstants.Discriminators.MultipleChoice)]
    public class MultipleChoiceTestTaskCreateEditCommand : TestTaskCreateEditCommand
    {
        public List<string> Options { get; set; }
        public int CorrectOptionIndex { get; set; }
    }
}