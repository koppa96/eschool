using System;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.Users.Students
{
    public class RemoveStudentFromClassCommand : IRequest
    {
        public Guid StudentId { get; set; }
    }
}