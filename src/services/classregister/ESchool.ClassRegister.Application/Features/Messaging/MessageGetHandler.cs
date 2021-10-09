using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESchool.ClassRegister.Application.Features.Messaging.Common;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.Messaging;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Messaging
{
    public class MessageGetHandler : IRequestHandler<MessageGetQuery, MessageDetailsResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public MessageGetHandler(ClassRegisterContext context, IMapper mapper, IIdentityService identityService)
        {
            this.context = context;
            this.mapper = mapper;
            this.identityService = identityService;
        }
        
        public async Task<MessageDetailsResponse> Handle(MessageGetQuery request, CancellationToken cancellationToken)
        {
            var message = await context.Messages.Include(x => x.SenderClassRegisterUser)
                .Include(x => x.ReceiverUserMessages)
                    .ThenInclude(x => x.ClassRegisterUser)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);

            var currentUserId = identityService.GetCurrentUserId();
            
            var receiverUserMessage = message.ReceiverUserMessages.SingleOrDefault(x => x.UserId == currentUserId);
            if (receiverUserMessage != null)
            {
                receiverUserMessage.IsRead = true;
            }
            
            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<MessageDetailsResponse>(message);
        }
    }
}