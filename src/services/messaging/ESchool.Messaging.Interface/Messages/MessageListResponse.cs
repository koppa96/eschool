using System;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.Messaging.Interface.Messages
{
    public class MessageListResponse
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
        
        public UserListResponse Sender { get; set; }
    }
}