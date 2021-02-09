using ESchool.Libs.Domain;
using ESchool.Libs.Domain.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace ESchool.Libs.AspNetCore.Extensions
{
    public static class AuthorizationOptionsExtensions
    {
        public static void AddTenantUserPolicy(this AuthorizationOptions options, TenantRoleType tenantRoleType)
        {
            options.AddPolicy(tenantRoleType.ToString(), policy => policy.RequireAuthenticatedUser()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireClaim(Constants.ClaimTypes.GlobalRole, nameof(GlobalRoleType.TenantUser))
                .RequireClaim(Constants.ClaimTypes.TenantRoles, tenantRoleType.ToString()));
        }
    }
}