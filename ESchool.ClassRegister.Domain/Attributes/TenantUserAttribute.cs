using System;
using ESchool.Libs.Domain.Enums;

namespace ESchool.ClassRegister.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class TenantUserAttribute : Attribute
    {
        public TenantRoleType TenantRoleType { get; }
        

        public TenantUserAttribute(TenantRoleType tenantRoleType)
        {
            TenantRoleType = tenantRoleType;
        }
    }
}