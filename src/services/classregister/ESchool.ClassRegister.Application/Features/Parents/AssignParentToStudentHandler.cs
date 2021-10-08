using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Users;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.Parents
{
    public class AssignParentToStudentCommand : IRequest
    {
        public Guid ParentId { get; set; }
        public Guid StudentId { get; set; }
    }
    
    public class AssignParentToStudentHandler : IRequestHandler<AssignParentToStudentCommand>
    {
        private readonly ClassRegisterContext context;

        public AssignParentToStudentHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public async Task<Unit> Handle(AssignParentToStudentCommand request, CancellationToken cancellationToken)
        {
            var studentParent = new StudentParent
            {
                StudentId = request.StudentId,
                ParentId = request.ParentId
            };

            context.Add(studentParent);
            await context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}