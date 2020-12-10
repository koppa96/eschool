using System;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Domain.Entities.Users;
using ESchool.Libs.Domain.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;

namespace ESchool.IdentityProvider
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var logger = host.Services.GetRequiredService<ILogger<Program>>();

            await Policy.Handle<Exception>()
                .WaitAndRetryForeverAsync(x => TimeSpan.FromSeconds(3), (exception, span) => logger.LogCritical(exception, "Failed to start up."))
                .ExecuteAsync(async () =>
                {
                    using var scope = host.Services.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<IdentityProviderContext>();
                    await context.Database.MigrateAsync();

                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                    var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                    if (!await userManager.Users.AnyAsync())
                    {
                        var defaultAdmin = new User
                        {
                            UserName = configuration.GetValue<string>("DefaultTenantAdministrator:UserName"),
                            Email = configuration.GetValue<string>("DefaultTenantAdministrator:Email"),
                            GlobalRole = GlobalRoleType.TenantAdministrator
                        };

                        var result = await userManager.CreateAsync(defaultAdmin,
                            configuration.GetValue<string>("DefaultTenantAdministrator:Password"));
                        if (!result.Succeeded)
                        {
                            throw new InvalidOperationException("Failed to create the default user.");
                        }
                    }
                });

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
