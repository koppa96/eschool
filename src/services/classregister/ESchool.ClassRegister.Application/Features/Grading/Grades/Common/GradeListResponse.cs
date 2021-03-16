using System;
using ESchool.ClassRegister.Application.Features.Grading.GradeKinds.Common;
using ESchool.ClassRegister.Domain.Enums;

namespace ESchool.ClassRegister.Application.Features.Grading.Grades.Common
{
    public class GradeListResponse
    {
        public Guid Id { get; set; }
        public GradeValue Value { get; set; }
        public GradeKindResponse GradeKind { get; set; }
    }
}