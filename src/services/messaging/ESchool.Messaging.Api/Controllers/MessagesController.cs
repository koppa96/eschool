using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.AspNetCore;
using ESchool.Libs.Interface.Response;
using ESchool.Messaging.Application.Features.Messages;
using ESchool.Messaging.Interface.Messages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Messaging.Api.Controllers
{
    [Authorize(PolicyNames.AnyRole)]
    [Route("api/messages")]
    public class MessagesController : ESchoolControllerBase
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

        [HttpGet("recipients")]
        public Task<PagedListResponse<RecipientDto>> ListRecipients([FromQuery] MessageRecipientListQuery query,
            CancellationToken cancellationToken)
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