using ESchool.Libs.AspNetCore.Configuration;
using ESchool.Libs.AspNetCore.Services;
using ESchool.Libs.Domain;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace ESchool.Libs.AspNetCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<IIdentityService, IdentityService>();

            return services;
        }

        public static IServiceCollection AddCommonAuthentication(this IServiceCollection services, AuthConfiguration authConfiguration)
        {
            services.AddAuthentication(config =>
                {
                    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(config =>
                {
                    config.Authority = authConfiguration.Authority;
                    config.Audience = authConfiguration.Audience;
                    config.RequireHttpsMetadata = false;
                });
            return services;
        }

        public static IServiceCollection AddCommonAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(nameof(GlobalRoleType.TenantAdministrator), policy => policy.RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireClaim(Constants.ClaimTypes.GlobalRole, nameof(GlobalRoleType.TenantAdministrator)));
                
                options.AddTenantUserPolicy(TenantRoleType.Administrator);
                options.AddTenantUserPolicy(TenantRoleType.Parent);
                options.AddTenantUserPolicy(TenantRoleType.Teacher);
                options.AddTenantUserPolicy(TenantRoleType.Student);
            });
            return services;
        }
    }
}
