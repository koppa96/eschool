﻿using ESchool.Testing.Domain.Entities.Tasks.MultipleChoice;
using System;

namespace ESchool.Testing.Domain.Entities.Answers.MultipleChoice
{
    public class MultipleChoiceTaskAnswer : TaskAnswer<MultipleChoiceTestTask>
    {
        public Guid? SelectedOptionId { get; set; }
        public virtual MultipleChoiceTestTaskOption SelectedOption { get; set; }

        public override void AutoCorrect()
        {
            AllOrNothingAutoCorrect(SelectedOptionId == TestTask.CorrectOptionId);
        }
    }
}
