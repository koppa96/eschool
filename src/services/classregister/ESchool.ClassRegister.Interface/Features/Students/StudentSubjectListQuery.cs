using System;
using ESchool.ClassRegister.Interface.Features.Subjects;
using ESchool.Libs.Interface.Query;

namespace ESchool.ClassRegister.Interface.Features.Students
{
    public class StudentSubjectListQuery : PagedListQuery<SubjectListResponse>
    {
        public Guid StudentId { get; set; }
        public Guid SchoolYearId { get; set; }
    }
}