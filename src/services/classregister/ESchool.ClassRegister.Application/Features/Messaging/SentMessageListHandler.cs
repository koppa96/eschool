using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Application.Features.Messaging.Common;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.Messaging;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Application.Cqrs.Query;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Messaging
{
    public class SentMessageListQuery : PagedListQuery<MessageListResponse>
    {
        
    }
    
    public class SentMessageListHandler : AutoMapperPagedListHandler<SentMessageListQuery, Message, MessageListResponse>
    {
        private readonly IIdentityService identityService;

        public SentMessageListHandler(
            ClassRegisterContext context,
            IConfigurationProvider provider,
            IIdentityService identityService) : base(context, provider)
        {
            this.identityService = identityService;
        }

        protected override IQueryable<Message> Filter(IQueryable<Message> entities, SentMessageListQuery query)
        {
            var currentUserId = identityService.GetCurrentUserId();
            return entities.Where(x => x.SenderUser.UserId == currentUserId);
        }

        protected override IOrderedQueryable<Message> Order(IQueryable<Message> entities)
        {
            return entities.OrderByDescending(x => x.SentAt);
        }
    }
}