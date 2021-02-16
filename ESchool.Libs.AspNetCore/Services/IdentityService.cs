using ESchool.Libs.Domain;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace ESchool.Libs.AspNetCore.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpContext httpContext;

        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            httpContext = httpContextAccessor.HttpContext;
        }

        public Guid GetCurrentUserId()
        {
            return Guid.Parse(httpContext.User.Claims.Single(x => x.Type == JwtClaimTypes.Subject).Value);
        }

        public Guid? TryGetTenantId()
        {
            var claim = httpContext.User.Claims.SingleOrDefault(x => x.Type == Constants.ClaimTypes.TenantId);
            return claim != null ? Guid.Parse(claim.Value) : null;
        }

        public Guid GetTenantId()
        {
            return Guid.Parse(httpContext.User.Claims.Single(x => x.Type == Constants.ClaimTypes.TenantId).Value);
        }

        public bool IsInGlobalRole(GlobalRoleType globalRoleType)
        {
            return httpContext.User.Claims.Single(x => x.Type == Constants.ClaimTypes.GlobalRole).Value == globalRoleType.ToString();
        }

        public bool IsInRole(TenantRoleType tenantRoleType)
        {
            var roleTypeAsString = tenantRoleType.ToString();
            return httpContext.User.Claims.Any(x => x.Type == Constants.ClaimTypes.TenantRoles && x.Value == roleTypeAsString);
        }
    }
}
