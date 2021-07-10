using System;
using MediatR;

namespace ESchool.ClassRegister.Interface.Features.Messaging
{
    public class MessageGetQuery : IRequest<MessageDetailsResponse>
    {
        public Guid Id { get; set; }
    }
}