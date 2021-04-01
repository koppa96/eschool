using System;
using ESchool.ClassRegister.Application.Features.Users.Common;

namespace ESchool.ClassRegister.Application.Features.Messaging.Common
{
    public class MessageListResponse
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public DateTime SentAt { get; set; }
        
        public UserRoleListResponse Sender { get; set; }
    }
}