using System.Linq;
using ESchool.Libs.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Libs.AspNetCore.Filters.TenantRole
{
    public class TenantRoleFilterAttribute : TypeFilterAttribute
    {
        public TenantRoleFilterAttribute(params TenantRoleType[] tenantRoleTypes) : base(typeof(TenantRoleFilter))
        {
            Arguments = new object[] { tenantRoleTypes.AsEnumerable() };
        }
    }
}