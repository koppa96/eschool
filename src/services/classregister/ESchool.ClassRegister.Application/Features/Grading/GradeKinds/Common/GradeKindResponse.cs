using System;

namespace ESchool.ClassRegister.Application.Features.Grading.GradeKinds.Common
{
    public class GradeKindResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double AverageMultiplier { get; set; }
    }
}