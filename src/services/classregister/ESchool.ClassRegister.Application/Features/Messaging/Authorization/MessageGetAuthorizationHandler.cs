using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Messaging.Authorization
{
    public class MessageGetAuthorizationHandler : IRequestAuthorizationHandler<MessageGetQuery>
    {
        private readonly ClassRegisterContext context;
        private readonly IIdentityService identityService;

        public MessageGetAuthorizationHandler(ClassRegisterContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<RequestAuthorizationResult> IsAuthorizedAsync(MessageGetQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            var message = await context.Messages.Include(x => x.SenderUser)
                .Include(x => x.ReceiverUserMessages)
                    .ThenInclude(x => x.User)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);

            if (message.SenderUser.UserId == currentUserId ||
                message.ReceiverUserMessages.Any(x => x.User.UserId == currentUserId))
            {
                return RequestAuthorizationResult.Success;
            }
            
            return RequestAuthorizationResult.Failure("Az üzenetet csak a címzettek és a küldő tekintheti meg.");
        }
    }
}