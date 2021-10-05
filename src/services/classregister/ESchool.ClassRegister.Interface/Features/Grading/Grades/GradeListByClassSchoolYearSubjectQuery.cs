using System;
using System.Collections.Generic;
using ESchool.Libs.Interface.Query;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.Grading.Grades
{
    public class GradeListByClassSchoolYearSubjectQuery : PagedListQuery<GradeListByClassSchoolYearSubjectResponse>
    {
        public Guid ClassId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid SchoolYearId { get; set; }
    }
}