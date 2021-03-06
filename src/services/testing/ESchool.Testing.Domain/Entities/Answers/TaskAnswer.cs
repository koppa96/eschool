﻿using ESchool.Testing.Domain.Entities.Tasks;
using System;
using ESchool.Libs.Domain.Interfaces;

namespace ESchool.Testing.Domain.Entities.Answers
{
    public abstract class TaskAnswer : IEntity
    {
        public Guid Id { get; set; }
        public int GivenPoints { get; protected set; }
        public bool HasBeenCorrected { get; protected set; }

        public Guid TestAnswerId { get; set; }
        public virtual TestAnswer TestAnswer { get; set; }
        
        public Guid? TestTaskId { get; set; }

        public abstract void AutoCorrect();

        public void ManualCorrect(int givenPoints)
        {
            GivenPoints = givenPoints;
            HasBeenCorrected = true;
        }
    }

    public abstract class TaskAnswer<TTask> : TaskAnswer
        where TTask : TestTask
    {
        public virtual TTask TestTask { get; set; }

        protected void AllOrNothingAutoCorrect(bool isCorrect)
        {
            GivenPoints = isCorrect ? TestTask.PointValue : TestTask.IncorrectAnswerPointValue;
            HasBeenCorrected = true;
        }
    }
}
