using System;
using ESchool.Libs.Interface.Query;

namespace ESchool.ClassRegister.Interface.Features.SubjectManagement.ClassSubjects
{
    public class ClassSubjectListQuery : PagedListQuery<ClassSubjectListResponse>
    {
        public Guid SchoolYearId { get; set; }
    }
}