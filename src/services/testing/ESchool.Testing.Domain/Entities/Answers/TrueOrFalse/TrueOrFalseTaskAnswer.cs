﻿using ESchool.Testing.Domain.Entities.Tasks.TrueOrFalse;

namespace ESchool.Testing.Domain.Entities.Answers.TrueOrFalse
{
    public class TrueOrFalseTaskAnswer : TaskAnswer<TrueOrFalseTestTask>
    {
        public bool IsTrue { get; set; }

        public override void AutoCorrect()
        {
            AllOrNothingAutoCorrect(IsTrue == TestTask.IsTrue);
        }
    }
}
