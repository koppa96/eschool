using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using AutoMapper;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Domain.Entities.Users;
using ESchool.IdentityProvider.Infrastructure;
using ESchool.Libs.AspNetCore.Configuration;
using ESchool.Libs.AspNetCore.Extensions;
using IdentityServer4.Services;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;
using ESchool.Libs.AspNetCore.Filters;
using ESchool.Libs.AspNetCore.Middlewares;
using ESchool.Libs.Outbox;
using ESchool.Libs.Outbox.AspNetCore.Extensions;
using ESchool.Libs.Outbox.EntityFrameworkCore.Extensions;
using NJsonSchema.Generation;

namespace ESchool.IdentityProvider
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
            services.AddDbContext<IdentityProviderContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.Configure<OutboxConfiguration>(Configuration.GetSection("Outbox"));
            services.AddMassTransitOutbox(config =>
            {
                config.UseEntityFrameworkCore<IdentityProviderContext>(efCoreConfig =>
                {
                    efCoreConfig.UseStandardMessageDispatcher();
                });
                
                config.AddPublishFilter<AuthDataSetterPublishFilter>();
            });

            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<IdentityProviderContext>();

            services.AddIdentityServer(config =>
                {
                    config.IssuerUri = Configuration.GetValue<string>("IdentityServer:IssuerUri");
                })
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(Configuration.GetSection("IdentityServer:IdentityResources"))
                .AddInMemoryApiResources(Configuration.GetSection("IdentityServer:ApiResources"))
                .AddInMemoryApiScopes(Configuration.GetSection("IdentityServer:ApiScopes"))
                .AddInMemoryClients(Configuration.GetSection("IdentityServer:Clients"))
                .AddAspNetIdentity<User>();
            services.AddTransient<IProfileService, ProfileService>();

            var authConfig = new AuthConfiguration();
            Configuration.GetSection("Authentication").Bind(authConfig);
            services.AddCommonAuthentication(authConfig);
            services.AddCommonAuthorization();

            services.AddAutoMapper(Assembly.Load("ESchool.IdentityProvider.Application"));

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: false));
                });
            
            services.AddRazorPages();

            services.AddMediatR(Assembly.Load("ESchool.IdentityProvider.Application"));

            services.AddOpenApiDocument(config =>
            {
                config.Title = "ESchool Identity Provider API";
                config.Description = "The REST API documentation of the Identity Provider microservice.";
#pragma warning disable 618
                config.DefaultEnumHandling = EnumHandling.String;
#pragma warning restore 618

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
                                { "identityproviderapi.readwrite", "identityproviderapi.readwrite" },
                                { "openid", "openid" },
                                { "profile", "profile" }
                            }
                        }
                    }
                });
                
                config.AddSecurity("JWT", new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });
                
                config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("OAuth2"));
                config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });
            
            var rabbitMqConfig = new RabbitMqConfiguration();
            Configuration.GetSection("RabbitMQ").Bind(rabbitMqConfig);
            services.AddMassTransit(configure =>
            {
                configure.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(rabbitMqConfig.Host, rabbitConfig =>
                    {
                        rabbitConfig.Username(rabbitMqConfig.Username);
                        rabbitConfig.Password(rabbitMqConfig.Password);
                    });

                    configurator.UseCustomFilters(context);
                });
            });

            services.AddCommonServices();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy => policy.WithOrigins(Configuration.GetSection("AllowedCorsOrigins").Get<string[]>())
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<RequestLoggerMiddleware>();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();
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

            app.UseIdentityServer();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
