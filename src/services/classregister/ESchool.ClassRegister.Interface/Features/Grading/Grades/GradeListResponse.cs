using System;
using ESchool.ClassRegister.Interface.Features.Grading.GradeKinds;
using ESchool.ClassRegister.SharedDomain.Enums;

namespace ESchool.ClassRegister.Interface.Features.Grading.Grades
{
    public class GradeListResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public GradeValue Value { get; set; }
        public DateTime WrittenIn { get; set; }
        public GradeKindResponse GradeKind { get; set; }
    }
}