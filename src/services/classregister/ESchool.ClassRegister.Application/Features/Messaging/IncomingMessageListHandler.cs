using System;
using System.Linq;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Messaging;
using ESchool.ClassRegister.Interface.Features.Messaging;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Domain.Services;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.ClassRegister.Application.Features.Messaging
{
    public class IncomingMessageListHandler : PagedListHandler<IncomingMessageListQuery, Message, MessageListResponse>
    {
        private readonly Guid currentUserId;

        public IncomingMessageListHandler(ClassRegisterContext context,
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
                Sender = new UserRoleListResponse
                {
                    Id = x.SenderClassRegisterUser.Id,
                    Name = x.SenderClassRegisterUser.Name
                },
                SentAt = x.SentAt,
                Subject = x.Subject,
                IsRead = x.ReceiverUserMessages.Single(userMessage => userMessage.UserId == currentUserId).IsRead
            });
        }

        protected override IOrderedQueryable<Message> Order(IQueryable<Message> entities, IncomingMessageListQuery query)
        {
            return entities.OrderBy(x => x.SentAt);
        }
    }
}