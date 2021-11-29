using System.Threading;
using System.Threading.Tasks;
using ESchool.Messaging.Domain;
using ESchool.Messaging.Domain.Entities;
using ESchool.Messaging.Interface.RecipientGroups;
using MediatR;

namespace ESchool.Messaging.Application.Features.RecipientGroups
{
    public class RecipientGroupMemberAddHandler : IRequestHandler<RecipientGroupMemberAddCommand>
    {
        private readonly MessagingContext context;

        public RecipientGroupMemberAddHandler(MessagingContext context)
        {
            this.context = context;
        }
        
        public async Task<Unit> Handle(RecipientGroupMemberAddCommand request, CancellationToken cancellationToken)
        {
            var membership = new RecipientGroupMember
            {
                RecipientGroupId = request.GroupId,
                MemberId = request.MemberId
            };

            context.RecipientGroupMembers.Add(membership);
            await context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}