using System;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.Parents
{
    public class AssignParentToStudentCommand : IRequest
    {
        public Guid ParentId { get; set; }
        public Guid StudentId { get; set; }
    }
}