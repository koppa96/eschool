using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Domain.Entities.Users;
using ESchool.Libs.Domain;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ESchool.IdentityProvider.Infrastructure
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<User> userManager;

        public ProfileService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var userId = Guid.Parse(context.Subject.Claims.Single(x => x.Type == "sub").Value);
            var user = await userManager.Users.Include(x => x.TenantUsers)
                    .ThenInclude(x => x.TenantUserRoles)
                .Include(x => x.TenantUsers)
                    .ThenInclude(x => x.Tenant)
                .Include(x => x.DefaultTenant)
                .SingleAsync(x => x.Id == userId);

            var tenantId = context.ValidatedRequest.Raw.Get(Constants.ClaimTypes.TenantId);
            context.IssuedClaims.Add(new Claim(Constants.ClaimTypes.GlobalRole, user.GlobalRole.ToString()));

            if (string.IsNullOrEmpty(tenantId))
            {
                if (user.DefaultTenantId.HasValue)
                {
                    context.IssuedClaims.Add(new Claim(Constants.ClaimTypes.TenantId, user.DefaultTenantId.ToString()));
                    context.IssuedClaims.AddRange(user.TenantUsers.Single(x => x.TenantId == user.DefaultTenantId).TenantUserRoles
                        .Select(x => new Claim(Constants.ClaimTypes.TenantRoles, x.TenantRole.ToString())));
                }
            }
            else
            {
                var tenantIdGuid = Guid.Parse(tenantId);
                var tenantUser = user.TenantUsers.SingleOrDefault(x => x.TenantId == tenantIdGuid);
                if (tenantUser != null)
                {
                    context.IssuedClaims.Add(new Claim(Constants.ClaimTypes.TenantId, tenantId));
                    context.IssuedClaims.AddRange(tenantUser.TenantUserRoles.Select(x => new Claim(Constants.ClaimTypes.TenantRoles, x.TenantRole.ToString())));
                }
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await userManager.GetUserAsync(context.Subject);
            context.IsActive = user != null;
        }
    }
}