using System;
using ESchool.ClassRegister.Domain.Entities.Users.Abstractions;

namespace ESchool.ClassRegister.Domain.Entities.Messaging
{
    public class RecipientGroupMember
    {
        public Guid Id { get; set; }

        public Guid RecipientGroupId { get; set; }
        public virtual RecipientGroup RecipientGroup { get; set; }

        public Guid MemberId { get; set; }
        public virtual ClassRegisterUser Member { get; set; }
    }
}