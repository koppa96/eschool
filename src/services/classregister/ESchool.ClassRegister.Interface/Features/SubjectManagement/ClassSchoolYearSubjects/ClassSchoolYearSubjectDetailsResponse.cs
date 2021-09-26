using System.Collections.Generic;
using ESchool.ClassRegister.Interface.Features.Classes;
using ESchool.ClassRegister.Interface.Features.SchoolYears;
using ESchool.ClassRegister.Interface.Features.Subjects;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.ClassRegister.Interface.Features.SubjectManagement.ClassSchoolYearSubjects
{
    public class ClassSchoolYearSubjectDetailsResponse
    {
        public SchoolYearListResponse SchoolYear { get; set; }
        public ClassListResponse Class { get; set; }
        public SubjectListResponse Subject { get; set; }
        public List<UserRoleListResponse> Teachers { get; set; }
    }
}