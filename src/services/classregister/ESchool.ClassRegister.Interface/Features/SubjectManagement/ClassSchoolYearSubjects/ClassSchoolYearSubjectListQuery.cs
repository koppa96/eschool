using System;
using ESchool.ClassRegister.Interface.Features.Subjects;
using ESchool.Libs.Interface.Query;

namespace ESchool.ClassRegister.Interface.Features.SubjectManagement.ClassSchoolYearSubjects
{
    public class ClassSchoolYearSubjectListQuery : PagedListQuery<SubjectListResponse>
    {
        public Guid ClassId { get; set; }
        public Guid SchoolYearId { get; set; }
        public Guid SubjectId { get; set; }
    }
}