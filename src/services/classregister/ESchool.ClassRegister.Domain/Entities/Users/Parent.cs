using System.Collections.Generic;
using ESchool.ClassRegister.Domain.Attributes;
using ESchool.ClassRegister.Domain.Entities.Users.Abstractions;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.MultiTenancy.Entities;

namespace ESchool.ClassRegister.Domain.Entities.Users
{
    [TenantUser(TenantRoleType.Parent)]
    public class Parent : ClassRegisterUserRole
    {
        public virtual ICollection<StudentParent> StudentParents { get; set; }
    }
}
