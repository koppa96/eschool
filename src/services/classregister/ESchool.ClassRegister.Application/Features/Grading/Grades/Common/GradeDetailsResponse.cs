using System;
using ESchool.ClassRegister.Application.Features.Grading.GradeKinds.Common;
using ESchool.ClassRegister.Application.Features.Subjects;
using ESchool.ClassRegister.Application.Features.Users.Common;
using ESchool.ClassRegister.Domain.Enums;

namespace ESchool.ClassRegister.Application.Features.Grading.Grades.Common
{
    public class GradeDetailsResponse
    {
        public Guid Id { get; set; }
        public GradeValue GradeValue { get; set; }
        public string Description { get; set; }
        public GradeKindResponse GradeKind { get; set; }
        public UserListResponse Student { get; set; }
        public UserListResponse Teacher { get; set; }
        public SubjectListResponse Subject { get; set; }
    }
}