using ESchool.ClassRegister.Domain.Entities.Users;
using System;
using System.Collections.Generic;

namespace ESchool.ClassRegister.Domain.Entities.Messaging
{
    public class Message
    {
        public Guid Id { get; set; }

        public string Subject { get; set; }
        public string Text { get; set; }
        public DateTime SentAt { get; set; }

        public Guid? SenderUserId { get; set; }
        public virtual UserBase SenderUser { get; set; }

        public virtual ICollection<UserMessage> ReceiverUserMessages { get; set; }
    }
}
