using System.Collections.Generic;
using MediatR;

namespace ESchool.Messaging.Interface.Messages
{
    public class MessageSendCommand : IRequest<MessageDetailsResponse>
    {
        public string Subject { get; set; }
        public string Text { get; set; }
        public List<RecipientDto> Recipients { get; set; }
    }
}