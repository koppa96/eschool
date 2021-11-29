using System;
using System.Collections.Generic;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.SubjectManagement.ClassSchoolYearSubjects
{
    public class ClassSchoolYearSubjectCreateCommand : IRequest
    {
        public Guid ClassId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid SchoolYearId { get; set; }
        public List<Guid> TeacherIds { get; set; }
    }
}