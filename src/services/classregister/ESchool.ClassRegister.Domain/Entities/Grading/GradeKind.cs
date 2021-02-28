using ESchool.Libs.Domain.Interfaces;
using System;

namespace ESchool.ClassRegister.Domain.Entities.Grading
{
    public class GradeKind
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double AverageMultiplier { get; set; }
    }
}
