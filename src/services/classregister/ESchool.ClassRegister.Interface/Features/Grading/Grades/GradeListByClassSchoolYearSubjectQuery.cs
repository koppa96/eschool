using System;
using System.Collections.Generic;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.Grading.Grades
{
    public class GradeListByClassSchoolYearSubjectQuery : IRequest<List<GradeListByClassSchoolYearSubjectResponse>>
    {
        public Guid ClassId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid SchoolYearId { get; set; }
    }
}