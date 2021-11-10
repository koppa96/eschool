using System.Collections.Generic;
using ESchool.Libs.Domain.Interfaces;
using ESchool.Libs.Domain.MultiTenancy.Entities;

namespace ESchool.Messaging.Domain.Entities
{
    public class MessagingUser : UserBase<MessagingUser, MessagingUserRole>, ISoftDelete
    {
        public virtual ICollection<Message> SentMessages { get; set; }
        public virtual ICollection<UserMessage> ReceivedMessages { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}