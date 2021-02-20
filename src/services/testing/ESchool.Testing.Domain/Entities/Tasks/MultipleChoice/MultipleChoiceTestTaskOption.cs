using System;

namespace ESchool.Testing.Domain.Entities.Tasks.MultipleChoice
{
    public class MultipleChoiceTestTaskOption
    {
        public Guid Id { get; set; }
        public string Value { get; set; }

        public Guid TestTaskId { get; set; }
        public virtual MultipleChoiceTestTask TestTask { get; set; }
    }
}
