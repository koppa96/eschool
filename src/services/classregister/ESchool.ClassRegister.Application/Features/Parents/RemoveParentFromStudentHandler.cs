using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Parents
{
    public class RemoveParentFromStudentCommand : IRequest
    {
        public Guid ParentId { get; set; }
        public Guid StudentId { get; set; }
    }
    
    public class RemoveParentFromStudentHandler : IRequestHandler<RemoveParentFromStudentCommand>
    {
        private readonly ClassRegisterContext context;

        public RemoveParentFromStudentHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public async Task<Unit> Handle(RemoveParentFromStudentCommand request, CancellationToken cancellationToken)
        {
            var studentParent = await context.StudentParents.SingleOrDefaultAsync(
                x => x.StudentId == request.StudentId && x.ParentId == request.ParentId, cancellationToken);

            if (studentParent != null)
            {
                context.StudentParents.Remove(studentParent);
                await context.SaveChangesAsync(cancellationToken);
            }
            
            return Unit.Value;
        }
    }
}