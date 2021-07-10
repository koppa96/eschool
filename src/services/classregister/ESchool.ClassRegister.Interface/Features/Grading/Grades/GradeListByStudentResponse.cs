using System.Collections.Generic;
using ESchool.ClassRegister.Interface.Features.Subjects;

namespace ESchool.ClassRegister.Interface.Features.Grading.Grades
{
    public class GradeListByStudentResponse
    {
        public SubjectListResponse Subject { get; set; }
        public List<GradeListResponse> Grades { get; set; }
    }
}