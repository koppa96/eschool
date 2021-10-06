using System;
using ESchool.Libs.Interface.Query;

namespace ESchool.ClassRegister.Interface.Features.Grading.Grades
{
    public class GradeListByStudentQuery : PagedListQuery<GradeListResponse>
    {
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid SchoolYearId { get; set; }
    }
}