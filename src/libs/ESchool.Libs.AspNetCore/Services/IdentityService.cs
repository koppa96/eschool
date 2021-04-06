using ESchool.Libs.Domain;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace ESchool.Libs.AspNetCore.Services
{
    internal class IdentityService : IIdentityService
    {
        private readonly MessagingIdentityService messagingIdentityService;
        private readonly HttpContext httpContext;

        public IdentityService(IHttpContextAccessor httpContextAccessor, MessagingIdentityService messagingIdentityService)
        {
            this.messagingIdentityService = messagingIdentityService;
            httpContext = httpContextAccessor.HttpContext;
        }

        public Guid? TryGetCurrentUserId()
        {
            if (httpContext != null)
            {
                var claim = httpContext.User.Claims.SingleOrDefault(x => x.Type == JwtClaimTypes.Subject);
                return claim != null ? Guid.Parse(claim.Value) : null;
            }

            return messagingIdentityService.UserId;
        }

        public Guid GetCurrentUserId()
        {
            return TryGetCurrentUserId()!.Value;
        }

        public Guid? TryGetTenantId()
        {
            if (httpContext != null)
            {
                var claim = httpContext.User.Claims.SingleOrDefault(x => x.Type == Constants.ClaimTypes.TenantId);
                return claim != null ? Guid.Parse(claim.Value) : null;
            }

            return messagingIdentityService.TenantId;
        }

        public Guid GetTenantId()
        {
            return TryGetTenantId()!.Value;
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
