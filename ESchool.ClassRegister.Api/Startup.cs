using System.Reflection;
using ESchool.ClassRegister.Domain;
using MassTransit;
using MassTransit.MultiBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ESchool.ClassRegister.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ClassRegisterContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddControllers();

            services.AddMassTransit(config =>
            {
                config.AddConsumers(Assembly.GetExecutingAssembly());
                config.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(Configuration.GetValue<string>("RabbitMQ:Host"));
                    configurator.ReceiveEndpoint("class-register", endpoint =>
                    {
                        endpoint.ConfigureConsumers(context);
                    });
                });
            });
            services.AddMassTransitHostedService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
