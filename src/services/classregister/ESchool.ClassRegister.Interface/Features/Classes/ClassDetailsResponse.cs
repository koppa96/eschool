using System;
using ESchool.ClassRegister.Interface.Features.ClassTypes;
using ESchool.ClassRegister.Interface.Features.SchoolYears;
using ESchool.ClassRegister.Interface.Features.Users;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.ClassRegister.Interface.Features.Classes
{
    public class ClassDetailsResponse
    {
        public Guid Id { get; set; }
        public int Grade { get; set; }
        public bool DidFinish { get; set; }
        public ClassTypeListResponse ClassType { get; set; }
        public UserRoleListResponse HeadTeacher { get; set; }
        public SchoolYearListResponse StartingSchoolYear { get; set; }
        public SchoolYearListResponse FinishingSchoolYear { get; set; }
    }
}