using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.Libs.Domain.MultiTenancy;
using ESchool.Libs.Outbox.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ESchool.ClassRegister.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var masterDbContext = scope.ServiceProvider.GetRequiredService<MasterDbContext>();
                await masterDbContext.Database.MigrateAsync();

                var tenants = await masterDbContext.Tenants.ToListAsync();
                var dbContextOptions =
                    scope.ServiceProvider.GetRequiredService<DbContextOptions<ClassRegisterContext>>();
                var outboxDbContext = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();
                foreach (var tenant in tenants)
                {
                    await using var tenantDbContext = new ClassRegisterContext(dbContextOptions, tenant, outboxDbContext);
                    await tenantDbContext.Database.MigrateAsync();
                }
            }
            
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
