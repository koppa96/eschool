using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.Libs.Domain.Services;
using ESchool.Messaging.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Messaging.Application.Features.Messages
{
    public class MessageGetQuery : IRequest<MessageDetailsResponse>
    {
        public Guid Id { get; set; }
    }
    
    public class MessageGetHandler : IRequestHandler<MessageGetQuery, MessageDetailsResponse>
    {
        private readonly MessagingContext context;
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public MessageGetHandler(MessagingContext context, IMapper mapper, IIdentityService identityService)
        {
            this.context = context;
            this.mapper = mapper;
            this.identityService = identityService;
        }
        
        public async Task<MessageDetailsResponse> Handle(MessageGetQuery request, CancellationToken cancellationToken)
        {
            var message = await context.Messages.Include(x => x.SenderUser)
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