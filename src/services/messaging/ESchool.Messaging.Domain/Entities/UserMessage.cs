using System;

namespace ESchool.Messaging.Domain.Entities
{
    public class UserMessage
    {
        public Guid Id { get; set; }

        public Guid? UserId { get; set; }
        public virtual MessagingUser ClassRegisterUser { get; set; }

        public Guid MessageId { get; set; }
        public virtual Message Message { get; set; }

        public bool IsRead { get; set; }
    }
}
