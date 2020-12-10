using System.Collections.Generic;
using ESchool.ClassRegister.Domain.Attributes;
using ESchool.Libs.Domain.Enums;

namespace ESchool.ClassRegister.Domain.Entities.Users
{
    [TenantUser(TenantRoleType.Parent)]
    public class Parent : UserBase
    {
        public virtual ICollection<StudentParent> StudentParents { get; set; }
    }
}
