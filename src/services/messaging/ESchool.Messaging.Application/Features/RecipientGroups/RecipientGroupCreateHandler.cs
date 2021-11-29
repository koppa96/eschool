using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Domain.Services;
using ESchool.Messaging.Domain;
using ESchool.Messaging.Domain.Entities;
using ESchool.Messaging.Interface.RecipientGroups;
using MediatR;

namespace ESchool.Messaging.Application.Features.RecipientGroups
{
    public class RecipientGroupCreateHandler : IRequestHandler<RecipientGroupCreateCommand>
    {
        private readonly MessagingContext context;
        private readonly IIdentityService identityService;

        public RecipientGroupCreateHandler(MessagingContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<Unit> Handle(RecipientGroupCreateCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var group = new RecipientGroup
            {
                Name = request.Name,
                UserId = currentUserId
            };

            context.RecipientGroups.Add(group);
            await context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}