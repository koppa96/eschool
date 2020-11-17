using System.Collections.Generic;
using System.Reflection;
using ESchool.ClassRegister.Domain;
using ESchool.Libs.Application.IntegrationEvents;
using ESchool.Libs.Application.IntegrationEvents.Core;
using MassTransit;
using MassTransit.MultiBus;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;

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

            services.AddMediatR(Assembly.Load("ESchool.ClassRegister.Application"));

            services.AddAuthentication()
                .AddJwtBearer(config =>
                {
                    config.Authority = Configuration.GetValue<string>("Authentication:Authority");
                    config.Audience = Configuration.GetValue<string>("Authentication:Audience");
                    config.RequireHttpsMetadata = false;
                });

            services.AddAuthorization(config =>
            {
                config.AddPolicy("Default", builder => builder.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .RequireClaim("scope", "classregisterapi.readwrite"));

                config.DefaultPolicy = config.GetPolicy("Default");
            });

            services.AddOpenApiDocument(config =>
            {
                config.Title = "ESchool Class Register API";
                config.Description = "The REST API documentation of the Class Register microservice.";

                config.AddSecurity("OAuth2", new OpenApiSecurityScheme
                {
                    OpenIdConnectUrl = $"{Configuration.GetValue<string>("Authentication:IdentityProviderUri")}/.well-known/openid-configuration",
                    Scheme = "Bearer",
                    Type = OpenApiSecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = $"{Configuration.GetValue<string>("Authentication:IdentityProviderUri")}/connect/authorize",
                            TokenUrl = $"{Configuration.GetValue<string>("Authentication:IdentityProviderUri")}/connect/token",
                            Scopes = new Dictionary<string, string>
                            {
                                { "classregisterapi.readwrite", "classregisterapi.readwrite" },
                                { "openid", "openid" },
                                { "profile", "profile" }
                            }
                        }
                    }
                });
                
                config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("OAuth2"));
            });

            services.AddMassTransit(config =>
            {
                config.AddConsumer<MediatREventConsumer<UserCreatedIntegrationEvent>>();
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

            app.UseOpenApi();
            app.UseSwaggerUi3(config =>
            {
                config.OAuth2Client = new OAuth2ClientSettings
                {
                    ClientId = "test",
                    UsePkceWithAuthorizationCodeGrant = true,
                    ScopeSeparator = " "
                };
            });

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
