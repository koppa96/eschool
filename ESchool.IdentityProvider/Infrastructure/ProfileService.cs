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
                .SingleAsync(x => x.Id == userId);
            
            context.IssuedClaims.Add(new Claim(Constants.ClaimTypes.GlobalRole, user.GlobalRole.ToString()));

            context.IssuedClaims.AddRange(user.TenantUsers.SelectMany(
                x => x.TenantUserRoles,
                (user, userRole) => new Claim("tenantUserRoles", $"{userRole.Id}:{userRole.TenantRole}")));
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await userManager.GetUserAsync(context.Subject);
            context.IsActive = user != null;
        }
    }
}