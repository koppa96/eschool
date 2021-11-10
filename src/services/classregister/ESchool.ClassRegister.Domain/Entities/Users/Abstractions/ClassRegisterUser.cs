using ESchool.Libs.Domain.Interfaces;
using ESchool.Libs.Domain.MultiTenancy.Entities;

namespace ESchool.ClassRegister.Domain.Entities.Users.Abstractions
{
    public class ClassRegisterUser : UserBase<ClassRegisterUser, ClassRegisterUserRole>, ISoftDelete
    {

        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public bool IsDeleted { get; set; }
    }
}
