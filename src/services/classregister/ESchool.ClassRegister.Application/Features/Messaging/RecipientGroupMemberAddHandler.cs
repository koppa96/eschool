using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Messaging;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.Messaging
{
    public class RecipientGroupMemberAddCommand : IRequest
    {
        public Guid GroupId { get; set; }
        public Guid MemberId { get; set; }
    }
    
    public class RecipientGroupMemberAddHandler : IRequestHandler<RecipientGroupMemberAddCommand>
    {
        private readonly ClassRegisterContext context;

        public RecipientGroupMemberAddHandler(ClassRegisterContext context)
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