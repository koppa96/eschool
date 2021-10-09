using System;
using System.Collections.Generic;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.Messaging
{
    public class MessageSendCommand : IRequest<MessageDetailsResponse>
    {
        public string Subject { get; set; }
        public string Text { get; set; }
        public List<RecipientDto> Recipients { get; set; }
    }
    
    public class RecipientDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public RecipientType Type { get; set; }
            
        public enum RecipientType
        {
            User,
            Group
        }
    }
}