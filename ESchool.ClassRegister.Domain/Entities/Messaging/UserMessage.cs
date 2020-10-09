using ESchool.ClassRegister.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.ClassRegister.Domain.Entities.Messaging
{
    public class UserMessage
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public virtual UserBase User { get; set; }

        public Guid MessageId { get; set; }
        public virtual Message Message { get; set; }

        public bool IsRead { get; set; }
    }
}
