using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ESchool.Libs.Application.Cqrs.Authorization;
using ESchool.Libs.Application.Cqrs.Authorization.PipelineBehaviors;
using ESchool.Libs.Application.Dtos;
using ESchool.Libs.AspNetCore.Configuration;
using ESchool.Libs.AspNetCore.Services;
using ESchool.Libs.Domain;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.MultiTenancy;
using ESchool.Libs.Domain.MultiTenancy.Entities;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace ESchool.Libs.AspNetCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddScoped<MessagingIdentityService>();

            return services;
        }

        public static IServiceCollection AddCommonAuthentication(this IServiceCollection services,
            AuthConfiguration authConfiguration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
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
                options.AddPolicy(nameof(GlobalRoleType.TenantAdministrator), policy => policy
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireClaim(Constants.ClaimTypes.GlobalRole, nameof(GlobalRoleType.TenantAdministrator)));

                options.AddTenantUserPolicy(TenantRoleType.Administrator);
                options.AddTenantUserPolicy(TenantRoleType.Parent);
                options.AddTenantUserPolicy(TenantRoleType.Teacher);
                options.AddTenantUserPolicy(TenantRoleType.Student);
                
                options.AddPolicy(PolicyNames.AdministratorOrTenantAdministrator, policy =>
                    policy.RequireAuthenticatedUser()
                        .RequireAssertion(context =>
                            context.User.HasClaim(
                                Constants.ClaimTypes.GlobalRole, GlobalRoleType.TenantAdministrator.ToString()) ||
                            context.User.HasClaim(
                                Constants.ClaimTypes.TenantRoles, TenantRoleType.Administrator.ToString()))
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme));

                options.AddPolicy(PolicyNames.AnyRole, policy => policy.RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme));

                options.AddPolicy(PolicyNames.TeacherOrAdministrator, policy => policy.RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireClaim(Constants.ClaimTypes.TenantId)
                    .RequireAssertion(context =>
                        context.User.HasClaim(Constants.ClaimTypes.TenantRoles, TenantRoleType.Teacher.ToString()) ||
                        context.User.HasClaim(Constants.ClaimTypes.TenantRoles, TenantRoleType.Administrator.ToString())));
                
                options.AddPolicy(PolicyNames.TeacherOrStudent, policy => policy.RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireClaim(Constants.ClaimTypes.TenantId)
                    .RequireAssertion(context =>
                        context.User.HasClaim(Constants.ClaimTypes.TenantRoles, TenantRoleType.Teacher.ToString()) ||
                        context.User.HasClaim(Constants.ClaimTypes.TenantRoles, TenantRoleType.Student.ToString())));
                
                options.AddPolicy(PolicyNames.StudentOrParent, policy => policy.RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireClaim(Constants.ClaimTypes.TenantId)
                    .RequireAssertion(context =>
                        context.User.HasClaim(Constants.ClaimTypes.TenantRoles, TenantRoleType.Parent.ToString()) ||
                        context.User.HasClaim(Constants.ClaimTypes.TenantRoles, TenantRoleType.Student.ToString())));

                options.AddPolicy("Default", policy => policy.RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme));
            });
            return services;
        }

        public static IServiceCollection AddMediatRAuthorization(this IServiceCollection services,
            params Assembly[] assemblies)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(AuthorizingPipelineBehaviour<,>));

            var authorizationHandlers = assemblies.SelectMany(x => x.GetTypes())
                .Where(x => x.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestAuthorizationHandler<>)))
                .ToList();

            foreach (var handler in authorizationHandlers)
            {
                var interfaces = handler.GetInterfaces().Where(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestAuthorizationHandler<>));

                foreach (var @interface in interfaces)
                {
                    services.AddScoped(@interface, handler);
                }
            }

            return services;
        }

        public static IServiceCollection AddMultitenancy<TContext>(this IServiceCollection services)
            where TContext : DbContext
        {
            return services.AddMultitenancy<TContext, DefaultTenantDbContextFactory<TContext>>();
        }

        public static IServiceCollection AddMultitenancy<TContext, TFactory>(this IServiceCollection services)
            where TContext : DbContext
            where TFactory : class, ITenantDbContextFactory<TContext>
        {
            services.AddScoped(provider =>
            {
                var identityService = provider.GetRequiredService<IIdentityService>();
                var masterDbContext = provider.GetRequiredService<MasterDbContext>();
                var memoryCache = provider.GetRequiredService<IMemoryCache>();

                var tenantId = identityService.TryGetTenantId();

                return tenantId == null
                    ? null
                    : memoryCache.GetOrCreate(tenantId.Value, entry => masterDbContext.Tenants.Find(tenantId.Value));
            });

            services.AddScoped<ITenantDbContextFactory<TContext>, TFactory>();
            return services;
        }

        public static IServiceCollection AddLazyDbContext<TContext>(this IServiceCollection services)
            where TContext : DbContext
        {
            return services.AddScoped(provider =>
            {
                return new Lazy<TContext>(() => provider.GetRequiredService<TContext>());
            });
        }
    }
}