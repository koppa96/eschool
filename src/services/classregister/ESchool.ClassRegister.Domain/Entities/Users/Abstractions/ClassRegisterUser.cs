using System.Collections.Generic;
using ESchool.ClassRegister.Domain.Entities.Messaging;
using ESchool.Libs.Domain.Interfaces;
using ESchool.Libs.Domain.MultiTenancy.Entities;

namespace ESchool.ClassRegister.Domain.Entities.Users.Abstractions
{
    public class ClassRegisterUser : UserBase<ClassRegisterUser, ClassRegisterUserRole>, ISoftDelete
    {

        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<UserMessage> ReceivedMessages { get; set; }
        public virtual ICollection<Message> SentMessages { get; set; }

        public bool IsDeleted { get; set; }
    }
}
