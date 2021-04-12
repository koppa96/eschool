using System;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.Libs.Domain.MultiTenancy;
using ESchool.Libs.Outbox.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;

namespace ESchool.ClassRegister.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var logger = host.Services.GetRequiredService<ILogger<Program>>();

            await Policy.Handle<Exception>()
                .WaitAndRetryForeverAsync(x => TimeSpan.FromSeconds(3),
                    (exception, span) => logger.LogCritical(exception, "Failed to start up."))
                .ExecuteAsync(async () =>
                {
                    using var scope = host.Services.CreateScope();
                    var masterDbContext = scope.ServiceProvider.GetRequiredService<MasterDbContext>();
                    await masterDbContext.Database.MigrateAsync();

                    var tenants = await masterDbContext.Tenants.ToListAsync();
                    var factory = scope.ServiceProvider
                        .GetRequiredService<ITenantDbContextFactory<ClassRegisterContext>>();
                    foreach (var tenant in tenants)
                    {
                        await using var tenantDbContext = factory.CreateContext(tenant);
                        await tenantDbContext.Database.MigrateAsync();
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
