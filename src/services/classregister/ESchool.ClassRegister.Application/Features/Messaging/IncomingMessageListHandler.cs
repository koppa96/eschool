using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Application.Features.Messaging.Common;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Messaging;
using ESchool.ClassRegister.Interface.Features.Messaging;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Domain.Services;

namespace ESchool.ClassRegister.Application.Features.Messaging
{
    public class IncomingMessageListHandler : AutoMapperPagedListHandler<IncomingMessageListQuery, Message, MessageListResponse>
    {
        private readonly IIdentityService identityService;

        public IncomingMessageListHandler(ClassRegisterContext context,
            IConfigurationProvider configurationProvider,
            IIdentityService identityService) : base(context, configurationProvider)
        {
            this.identityService = identityService;
        }

        protected override IQueryable<Message> Filter(IQueryable<Message> entities, IncomingMessageListQuery query)
        {
            var currentUserId = identityService.GetCurrentUserId();
            return entities.Where(x => x.ReceiverUserMessages.Any(r => r.ClassRegisterUser.Id == currentUserId));
        }

        protected override IOrderedQueryable<Message> Order(IQueryable<Message> entities)
        {
            return entities.OrderBy(x => x.SentAt);
        }
    }
}