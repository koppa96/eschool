using System;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.Parents
{
    public class RemoveParentFromStudentCommand : IRequest
    {
        public Guid ParentId { get; set; }
        public Guid StudentId { get; set; }
    }
}