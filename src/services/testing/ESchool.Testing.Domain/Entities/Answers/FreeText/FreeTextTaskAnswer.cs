﻿using ESchool.Testing.Domain.Entities.Tasks.FreeText;

namespace ESchool.Testing.Domain.Entities.Answers.FreeText
{
    public class FreeTextTaskAnswer : TaskAnswer<FreeTextTestTask>
    {
        public string Answer { get; set; }

        public override void AutoCorrect()
        {
            // This task is not autocorrected, so this will do nothing
        }
    }
}
