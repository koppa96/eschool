using System;

namespace ESchool.Messaging.Domain.Entities
{
    public class RecipientGroupMember
    {
        public Guid Id { get; set; }

        public Guid RecipientGroupId { get; set; }
        public virtual RecipientGroup RecipientGroup { get; set; }

        public Guid MemberId { get; set; }
        public virtual MessagingUser Member { get; set; }
    }
}