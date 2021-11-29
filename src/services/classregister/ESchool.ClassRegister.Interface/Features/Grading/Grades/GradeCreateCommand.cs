using System;
using ESchool.ClassRegister.SharedDomain.Enums;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.Grading.Grades
{
    public class GradeCreateCommand : IRequest<GradeDetailsResponse>
    {
        public GradeValue Value { get; set; }
        public string Description { get; set; }
        public Guid GradeKindId { get; set; }
        public DateTime WrittenIn { get; set; }
        public Guid StudentId { get; set; }
        public Guid SchoolYearId { get; set; }
        public Guid SubjectId { get; set; }
    }
}