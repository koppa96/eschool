using System;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.SubjectManagement.Absences
{
    public class AbsenceCreateCommand : IRequest
    {
        public Guid LessonId { get; set; }
        public Guid StudentId { get; set; }
    }
}