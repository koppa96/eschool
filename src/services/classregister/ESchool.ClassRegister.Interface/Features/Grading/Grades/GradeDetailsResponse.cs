using System;
using ESchool.ClassRegister.Interface.Features.Grading.GradeKinds;
using ESchool.ClassRegister.Interface.Features.Subjects;
using ESchool.ClassRegister.Interface.Features.Users;
using ESchool.ClassRegister.SharedDomain.Enums;

namespace ESchool.ClassRegister.Interface.Features.Grading.Grades
{
    public class GradeDetailsResponse
    {
        public Guid Id { get; set; }
        public GradeValue GradeValue { get; set; }
        public string Description { get; set; }
        public GradeKindResponse GradeKind { get; set; }
        public UserRoleListResponse Student { get; set; }
        public UserRoleListResponse Teacher { get; set; }
        public SubjectListResponse Subject { get; set; }
    }
}