using System.Collections.Generic;
using ESchool.ClassRegister.Interface.Features.Users;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.ClassRegister.Interface.Features.Grading.Grades
{
    public class GradeListByClassSchoolYearSubjectResponse
    {
        public UserRoleListResponse Student { get; set; }
        public List<GradeListResponse> Grades { get; set; }
    }
}