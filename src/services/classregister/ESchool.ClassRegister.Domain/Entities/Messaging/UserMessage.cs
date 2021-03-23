using ESchool.ClassRegister.Domain.Entities.Users;
using System;
using ESchool.ClassRegister.Domain.Entities.Users.Abstractions;

namespace ESchool.ClassRegister.Domain.Entities.Messaging
{
    public class UserMessage
    {
        public Guid Id { get; set; }

        public Guid? UserId { get; set; }
        public virtual ClassRegisterUser ClassRegisterUser { get; set; }

        public Guid MessageId { get; set; }
        public virtual Message Message { get; set; }

        public bool IsRead { get; set; }
    }
}
