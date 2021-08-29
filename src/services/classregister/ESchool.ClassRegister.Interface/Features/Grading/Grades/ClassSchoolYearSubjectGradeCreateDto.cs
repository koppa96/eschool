using System;
using ESchool.ClassRegister.SharedDomain.Enums;

namespace ESchool.ClassRegister.Interface.Features.Grading.Grades
{
    public class ClassSchoolYearSubjectGradeCreateDto
    {
        public GradeValue Value { get; set; }
        public string Description { get; set; }
        public Guid GradeKindId { get; set; }
    }
}