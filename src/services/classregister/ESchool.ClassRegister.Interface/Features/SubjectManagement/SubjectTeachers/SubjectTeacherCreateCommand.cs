using System;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.SubjectManagement.SubjectTeachers
{
    public class SubjectTeacherCreateCommand : IRequest
    {
        public Guid SubjectId { get; set; }
        public Guid TeacherId { get; set; }
    }
}