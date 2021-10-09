using System;
using System.Collections.Generic;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.Messaging
{
    public class MessageSendCommand : IRequest<MessageDetailsResponse>
    {
        public string Subject { get; set; }
        public string Text { get; set; }
        public List<Recipient> Recipients { get; set; }
        
        public class Recipient
        {
            public Guid Id { get; set; }
            public RecipientType Type { get; set; }
            
            public enum RecipientType
            {
                User,
                Group
            }
        }
    }
}