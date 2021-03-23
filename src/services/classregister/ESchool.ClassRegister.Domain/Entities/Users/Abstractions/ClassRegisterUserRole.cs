using ESchool.Libs.Domain.Interfaces;
using ESchool.Libs.Domain.MultiTenancy.Entities;

namespace ESchool.ClassRegister.Domain.Entities.Users.Abstractions
{
    public class ClassRegisterUserRole : UserRoleBase<ClassRegisterUser, ClassRegisterUserRole>, ISoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}