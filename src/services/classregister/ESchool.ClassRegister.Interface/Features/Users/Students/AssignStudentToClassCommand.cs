using System;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.Users.Students
{
    public class AssignStudentToClassCommand : IRequest
    {
        public Guid StudentId { get; set; }
        public Guid ClassId { get; set; }
    }
}