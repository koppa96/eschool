using System;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.Users.Teachers
{
    public class AssignTeacherToClassSchoolYearSubjectCommand : IRequest
    {
        public Guid ClassId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid SchoolYearId { get; set; }
        public Guid TeacherId { get; set; }
    }
}