using System;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.SubjectManagement.ClassSchoolYearSubjects
{
    public class ClassSchoolYearSubjectDeleteCommand : IRequest
    {
        public Guid ClassId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid SchoolYearId { get; set; }
    }
}