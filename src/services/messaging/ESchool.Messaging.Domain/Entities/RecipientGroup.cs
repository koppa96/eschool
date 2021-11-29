using System;
using System.Collections.Generic;
using ESchool.Libs.Domain.Interfaces;

namespace ESchool.Messaging.Domain.Entities
{
    public class RecipientGroup : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid UserId { get; set; }
        public virtual MessagingUser User { get; set; }

        public virtual ICollection<RecipientGroupMember> Members { get; set; }
    }
}