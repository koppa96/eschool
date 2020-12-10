using System.Collections.Generic;
using System.Linq;
using ESchool.Libs.Domain;
using ESchool.Libs.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ESchool.Libs.AspNetCore.Filters.TenantRole
{
    public class TenantRoleFilter : IAuthorizationFilter
    {
        private readonly IEnumerable<TenantRoleType> tenantRoleTypes;

        public TenantRoleFilter(IEnumerable<TenantRoleType> tenantRoleTypes)
        {
            this.tenantRoleTypes = tenantRoleTypes;
        }
        
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Claims.Any(x =>
                x.Type == Constants.ClaimTypes.TenantRoles && tenantRoleTypes.Any(r => r.ToString() == x.Value)))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}