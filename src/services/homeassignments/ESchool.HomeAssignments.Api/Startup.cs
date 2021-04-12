using System;
using System.Reflection;
using ESchool.ClassRegister.Grpc;
using ESchool.HomeAssignments.Api.Infrastructure;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Services;
using ESchool.IdentityProvider.Interface.DefaultHandlers.Extensions;
using ESchool.Libs.AspNetCore.Configuration;
using ESchool.Libs.AspNetCore.Extensions;
using ESchool.Libs.AspNetCore.Filters;
using ESchool.Libs.Domain.MultiTenancy;
using ESchool.Libs.Outbox.AspNetCore.Extensions;
using ESchool.Libs.Outbox.EntityFrameworkCore.Extensions;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ESchool.HomeAssignments.Api
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
            services.AddMemoryCache();
            services.AddDbContext<HomeAssignmentsContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddLazyDbContext<HomeAssignmentsContext>();
            
            services.AddDbContext<MasterDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MasterDbConnection"), config =>
                    config.MigrationsAssembly(typeof(HomeAssignmentsContext).Assembly.GetName().Name)));

            services.AddControllers();

            var authConfig = new AuthConfiguration();
            Configuration.GetSection("Authentication").Bind(authConfig);
            services.AddCommonAuthentication(authConfig);
            services.AddCommonAuthorization();

            services.AddCommonServices();
            services.AddMultitenancy<HomeAssignmentsContext>();

            services.AddMediatR(Assembly.Load("ESchool.HomeAssignments.Application"))
                .AddMediatRAuthorization(Assembly.Load("ESchool.HomeAssignments.Application"));

            services.AddMassTransit(config =>
            {
                config.AddConsumers(Assembly.Load("ESchool.HomeAssignments.Application"));
                config.AddTenantEventConsumers<HomeAssignmentsContext>();
                config.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(Configuration.GetValue<string>("RabbitMQ:Host"));
                    configurator.ReceiveEndpoint("home-assignments", endpoint =>
                    {
                        endpoint.ConfigureConsumers(context);
                    });
                });
            });
            services.AddMassTransitHostedService();

            services.AddMassTransitOutbox(options =>
            {
                options.UseEntityFrameworkCore<HomeAssignmentsContext>(config =>
                {
                    config.UseMultiTenantMessageDispatcher();
                });
                options.AddPublishFilter<AuthDataSetterPublishFilter>();
            });

            services.AddMultitenancy<HomeAssignmentsContext>();
            services.AddTransient<ISolutionFileHandlerService, LocalSolutionFileHandlerService>();
            
            services.AddAutoMapper(Assembly.Load("ESchool.HomeAssignments.Application"));

            services.AddGrpcClient<LessonService.LessonServiceClient>(config =>
            {
                config.Address = new Uri(Configuration.GetValue<string>("ClassRegisterUri"));
            });

            services.AddGrpcClient<UserService.UserServiceClient>(config =>
            {
                config.Address = new Uri(Configuration.GetValue<string>("ClassRegisterUri"));
            });

            services.AddGrpcClient<ClassSchoolYearSubjectService.ClassSchoolYearSubjectServiceClient>(config =>
            {
                config.Address = new Uri(Configuration.GetValue<string>("ClassRegisterUri"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
