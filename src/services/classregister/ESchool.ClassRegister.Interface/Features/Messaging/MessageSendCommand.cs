using System;
using System.Collections.Generic;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.Messaging
{
    public class MessageSendCommand : IRequest<MessageDetailsResponse>
    {
        public string Subject { get; set; }
        public string Text { get; set; }
        public List<Guid> RecipientIds { get; set; }
    }
}