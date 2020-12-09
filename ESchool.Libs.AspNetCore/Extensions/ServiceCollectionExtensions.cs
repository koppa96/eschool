using ESchool.Libs.AspNetCore.Services;
using ESchool.Libs.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ESchool.Libs.AspNetCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            services.AddTransient<IIdentityService, IdentityService>();

            return services;
        }
    }
}
