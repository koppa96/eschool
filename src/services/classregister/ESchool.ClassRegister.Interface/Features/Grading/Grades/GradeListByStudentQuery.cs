using System;
using System.Collections.Generic;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.Grading.Grades
{
    public class GradeListByStudentQuery : IRequest<List<GradeListByStudentResponse>>
    {
        public Guid StudentId { get; set; }
        public Guid SchoolYearId { get; set; }
    }
}