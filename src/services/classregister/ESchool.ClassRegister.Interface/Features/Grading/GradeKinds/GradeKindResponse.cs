using System;

namespace ESchool.ClassRegister.Interface.Features.Grading.GradeKinds
{
    public class GradeKindResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double AverageMultiplier { get; set; }
    }
}