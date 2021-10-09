using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Messaging
{
    public class RecipientGroupMemberRemoveCommand : IRequest
    {
        public Guid GroupId { get; set; }
        public Guid MemberId { get; set; }
    }
    
    public class RecipientGroupMemberRemoveHandler : IRequestHandler<RecipientGroupMemberRemoveCommand>
    {
        private readonly ClassRegisterContext context;

        public RecipientGroupMemberRemoveHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public async Task<Unit> Handle(RecipientGroupMemberRemoveCommand request, CancellationToken cancellationToken)
        {
            var membership = await context.RecipientGroupMembers.SingleOrDefaultAsync(
                x => x.MemberId == request.MemberId && x.RecipientGroupId == request.GroupId, cancellationToken);

            if (membership != null)
            {
                context.RecipientGroupMembers.Remove(membership);
                await context.SaveChangesAsync(cancellationToken);
            }
            
            return Unit.Value;
        }
    }
}