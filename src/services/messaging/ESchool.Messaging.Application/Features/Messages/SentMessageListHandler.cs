using System;
using System.Linq;
using AutoMapper;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Domain.Services;
using ESchool.Libs.Interface.Query;
using ESchool.Libs.Interface.Response.Common;
using ESchool.Messaging.Domain;
using ESchool.Messaging.Domain.Entities;

namespace ESchool.Messaging.Application.Features.Messages
{
    public class SentMessageListQuery : PagedListQuery<MessageListResponse>
    {
        
    }
    
    public class MessageListResponse
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
        
        public UserListResponse Sender { get; set; }
    }
    
    public class SentMessageListHandler : AutoMapperPagedListHandler<SentMessageListQuery, Message, MessageListResponse>
    {
        private readonly IIdentityService identityService;

        public SentMessageListHandler(
            MessagingContext context,
            IConfigurationProvider provider,
            IIdentityService identityService) : base(context, provider)
        {
            this.identityService = identityService;
        }

        protected override IQueryable<Message> Filter(IQueryable<Message> entities, SentMessageListQuery query)
        {
            var currentUserId = identityService.GetCurrentUserId();
            return entities.Where(x => x.SenderUserId == currentUserId);
        }

        protected override IOrderedQueryable<Message> Order(IQueryable<Message> entities, SentMessageListQuery query)
        {
            return entities.OrderByDescending(x => x.SentAt);
        }
    }
}