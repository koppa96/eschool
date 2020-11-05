using ESchool.ClassRegister.Domain.Attributes;
using ESchool.Libs.Domain.Enums;

namespace ESchool.ClassRegister.Domain.Entities.Users
{
    [TenantUser(TenantRoleType.Administrator)]
    public class Administrator : UserBase
    {
    }
}