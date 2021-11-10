using System;
using System.Collections.Generic;

namespace ESchool.Messaging.Domain.Entities
{
    public class Message
    {
        public Guid Id { get; set; }

        public string Subject { get; set; }
        public string Text { get; set; }
        public DateTime SentAt { get; set; }

        public Guid? SenderUserId { get; set; }
        public virtual MessagingUser SenderUser { get; set; }

        public virtual ICollection<UserMessage> ReceiverUserMessages { get; set; }
    }
}
