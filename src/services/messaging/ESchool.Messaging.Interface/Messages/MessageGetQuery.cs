using System;
using MediatR;

namespace ESchool.Messaging.Interface.Messages
{
    public class MessageGetQuery : IRequest<MessageDetailsResponse>
    {
        public Guid Id { get; set; }
    }
}