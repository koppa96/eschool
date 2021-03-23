using ESchool.ClassRegister.Domain.Attributes;
using ESchool.ClassRegister.Domain.Entities.Users.Abstractions;
using ESchool.Libs.Domain.Enums;

namespace ESchool.ClassRegister.Domain.Entities.Users
{
    [TenantUser(TenantRoleType.Administrator)]
    public class Administrator : ClassRegisterUserRole
    {
    }
}