using System.Collections.Generic;
using System.Linq;
using ESchool.Libs.Domain;
using ESchool.Libs.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ESchool.Libs.AspNetCore.Filters.GlobalRole
{
    public class GlobalRoleFilter : IAuthorizationFilter
    {
        private readonly IEnumerable<GlobalRoleType> globalRoleTypes;

        public GlobalRoleFilter(IEnumerable<GlobalRoleType> globalRoleTypes)
        {
            this.globalRoleTypes = globalRoleTypes;
        }
        
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Claims.Any(x => x.Type == Constants.ClaimTypes.GlobalRole && globalRoleTypes.Any(r => r.ToString() == x.Value)))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}