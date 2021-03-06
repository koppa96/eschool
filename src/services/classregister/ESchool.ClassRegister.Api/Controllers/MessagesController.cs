﻿using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.Messaging;
using ESchool.ClassRegister.Application.Features.Messaging.Common;
using ESchool.Libs.Application.Cqrs.Response;
using ESchool.Libs.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.ClassRegister.Api.Controllers
{
    [Authorize(PolicyNames.AnyRole)]
    [ApiController]
    [Route("api/messages")]
    public class MessagesController : ControllerBase
    {
        private readonly IMediator mediator;

        public MessagesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("incoming")]
        public Task<PagedListResponse<MessageListResponse>> ListIncomingMessages(
            [FromQuery] IncomingMessageListQuery query, CancellationToken cancellationToken)
        {
            return mediator.Send(query, cancellationToken);
        }

        [HttpGet("sent")]
        public Task<PagedListResponse<MessageListResponse>> ListSentMessages(
            [FromQuery] SentMessageListQuery query, CancellationToken cancellationToken)
        {
            return mediator.Send(query, cancellationToken);
        }

        [HttpPost]
        public Task<MessageDetailsResponse> SendMessage([FromBody] MessageSendCommand command,
            CancellationToken cancellationToken)
        {
            return mediator.Send(command, cancellationToken);
        }

        [HttpGet("{messageId}")]
        public Task<MessageDetailsResponse> GetMessage(Guid messageId, CancellationToken cancellationToken)
        {
            return mediator.Send(new MessageGetQuery
            {
                Id = messageId
            }, cancellationToken);
        }
    }
}