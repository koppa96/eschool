using System;
using System.Collections.Generic;
using ESchool.ClassRegister.Domain.Entities.Messaging;
using ESchool.Libs.Domain.Interfaces;

namespace ESchool.ClassRegister.Domain.Entities.Users
{
    public class UserBase : ISoftDelete
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<UserMessage> ReceivedMessages { get; set; }
        public virtual ICollection<Message> SentMessages { get; set; }
        
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
