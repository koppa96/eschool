using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Domain.Services;
using ESchool.Messaging.Domain;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Messaging.Application.Features.Messages.Authorization
{
    public class MessageGetAuthorizationHandler : IRequestAuthorizationHandler<MessageGetQuery>
    {
        private readonly MessagingContext context;
        private readonly IIdentityService identityService;

        public MessageGetAuthorizationHandler(MessagingContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(MessageGetQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var message = await context.Messages.Include(x => x.SenderUser)
                .Include(x => x.ReceiverUserMessages)
                    .ThenInclude(x => x.ClassRegisterUser)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);

            if (message.SenderUserId == currentUserId ||
                message.ReceiverUserMessages.Any(x => x.ClassRegisterUser.Id == currentUserId))
            {
                return RequestAuthorizationResult.Success;
            }
            
            return RequestAuthorizationResult.Failure("Az üzenetet csak a címzettek és a küldő tekintheti meg.");
        }
    }
}