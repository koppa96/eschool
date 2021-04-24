using System;
using System.Collections.Generic;
using ESchool.Testing.Domain.Enums;

namespace ESchool.Testing.Domain.Entities.Tasks.MultipleChoice
{
    public class MultipleChoiceTestTask : TestTask
    {
        public virtual ICollection<MultipleChoiceTestTaskOption> Options { get; set; }

        public Guid? CorrectOptionId { get; set; }
        public virtual MultipleChoiceTestTaskOption CorrectOption { get; set; }
    }
}
