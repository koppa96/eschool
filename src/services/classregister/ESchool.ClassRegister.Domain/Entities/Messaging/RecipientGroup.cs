using System;
using System.Collections.Generic;
using ESchool.ClassRegister.Domain.Entities.Users.Abstractions;
using ESchool.Libs.Domain.Interfaces;

namespace ESchool.ClassRegister.Domain.Entities.Messaging
{
    public class RecipientGroup : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid UserId { get; set; }
        public virtual ClassRegisterUser User { get; set; }

        public virtual ICollection<RecipientGroupMember> Members { get; set; }
    }
}