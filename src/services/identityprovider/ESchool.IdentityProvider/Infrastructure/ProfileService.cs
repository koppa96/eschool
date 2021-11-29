using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Domain.Entities.Users;
using ESchool.Libs.Domain;
using ESchool.Libs.Domain.Enums;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ESchool.IdentityProvider.Infrastructure
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<User> userManager;
        private readonly IdentityProviderContext dbContext;

        public ProfileService(UserManager<User> userManager, IdentityProviderContext dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
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

            if (user.GlobalRole == GlobalRoleType.TenantUser)
            {
                TenantUser tenantUser = null;

                if (string.IsNullOrEmpty(tenantId))
                {

                    if (user.DefaultTenantId.HasValue)
                    {
                        tenantUser = user.TenantUsers.Single(x => x.TenantId == user.DefaultTenantId.Value);
                    }
                    else if (user.TenantUsers.Any())
                    {
                        tenantUser = user.TenantUsers.First();
                    }

                    context.IssuedClaims.Add(new Claim(Constants.ClaimTypes.TenantId, tenantUser.TenantId.ToString()));
                    context.IssuedClaims.AddRange(tenantUser.TenantUserRoles
                        .Select(x => new Claim(Constants.ClaimTypes.TenantRoles, x.TenantRole.ToString())));
                }
                else
                {
                    var tenantIdGuid = Guid.Parse(tenantId);
                    tenantUser = user.TenantUsers.SingleOrDefault(x => x.TenantId == tenantIdGuid);
                    if (tenantUser != null)
                    {
                        context.IssuedClaims.Add(new Claim(Constants.ClaimTypes.TenantId, tenantId));
                        context.IssuedClaims.AddRange(tenantUser.TenantUserRoles.Select(x => new Claim(Constants.ClaimTypes.TenantRoles, x.TenantRole.ToString())));
                    }
                }

                user.DefaultTenant = tenantUser.Tenant;
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await userManager.GetUserAsync(context.Subject);
            context.IsActive = user != null;
        }
    }
}