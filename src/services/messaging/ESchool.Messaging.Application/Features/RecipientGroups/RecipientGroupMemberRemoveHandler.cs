using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Messaging.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Messaging.Application.Features.RecipientGroups
{
    public class RecipientGroupMemberRemoveCommand : IRequest
    {
        public Guid GroupId { get; set; }
        public Guid MemberId { get; set; }
    }
    
    public class RecipientGroupMemberRemoveHandler : IRequestHandler<RecipientGroupMemberRemoveCommand>
    {
        private readonly MessagingContext context;

        public RecipientGroupMemberRemoveHandler(MessagingContext context)
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