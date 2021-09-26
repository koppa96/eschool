using System;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.SubjectManagement.ClassSchoolYearSubjects
{
    public class ClassSchoolYearSubjectQuery : IRequest<ClassSchoolYearSubjectDetailsResponse>
    {
        public Guid ClassId { get; set; }
        public Guid SchoolYearId { get; set; }
        public Guid SubjectId { get; set; }
    }
}