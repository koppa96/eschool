using System;
using System.Linq;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Domain.Services;
using ESchool.Libs.Interface.Response.Common;
using ESchool.Messaging.Domain;
using ESchool.Messaging.Domain.Entities;
using ESchool.Messaging.Interface.Messages;

namespace ESchool.Messaging.Application.Features.Messages
{
    public class IncomingMessageListHandler : PagedListHandler<IncomingMessageListQuery, Message, MessageListResponse>
    {
        private readonly Guid currentUserId;

        public IncomingMessageListHandler(MessagingContext context,
            IIdentityService identityService) : base(context)
        {
            currentUserId = identityService.GetCurrentUserId();
        }

        protected override IQueryable<Message> Filter(IQueryable<Message> entities, IncomingMessageListQuery query)
        {
            return entities.Where(x => x.ReceiverUserMessages.Any(r => r.ClassRegisterUser.Id == currentUserId));
        }

        protected override IQueryable<MessageListResponse> Map(IQueryable<Message> entities, IncomingMessageListQuery query)
        {
            return entities.Select(x => new MessageListResponse
            {
                Id = x.Id,
                Sender = new UserListResponse
                {
                    Id = x.SenderUser.Id,
                    Name = x.SenderUser.Name
                },
                SentAt = x.SentAt,
                Subject = x.Subject,
                IsRead = x.ReceiverUserMessages.Single(userMessage => userMessage.UserId == currentUserId).IsRead
            });
        }

        protected override IOrderedQueryable<Message> Order(IQueryable<Message> entities, IncomingMessageListQuery query)
        {
            return entities.OrderByDescending(x => x.SentAt);
        }
    }
}