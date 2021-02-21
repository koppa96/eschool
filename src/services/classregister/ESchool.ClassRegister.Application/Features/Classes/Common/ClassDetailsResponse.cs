using System;
using ESchool.ClassRegister.Application.Features.ClassTypes.Common;
using ESchool.ClassRegister.Application.Features.SchoolYears;
using ESchool.ClassRegister.Application.Features.Users.Common;

namespace ESchool.ClassRegister.Application.Features.Classes.Common
{
    public class ClassDetailsResponse
    {
        public Guid Id { get; set; }
        public int Grade { get; set; }
        public bool DidFinish { get; set; }
        public ClassTypeListResponse ClassType { get; set; }
        public UserListResponse HeadTeacher { get; set; }
        public SchoolYearListResponse StartingSchoolYear { get; set; }
        public SchoolYearListResponse FinishingSchoolYear { get; set; }
    }
}