﻿using System;
using ESchool.Libs.Domain.Interfaces;
using ESchool.Testing.Domain.Enums;

namespace ESchool.Testing.Domain.Entities.Tasks
{
    public abstract class TestTask : IEntity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        public int PointValue { get; set; }
        public int IncorrectAnswerPointValue { get; set; }

        public Guid TestId { get; set; }
        public virtual Test Test { get; set; }
    }
}
