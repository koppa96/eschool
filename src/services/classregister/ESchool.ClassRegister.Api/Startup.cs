using System;
using System.Collections.Generic;
using System.Reflection;
using ESchool.ClassRegister.Api.Grpc;
using ESchool.ClassRegister.Api.Infrastructure;
using ESchool.ClassRegister.Domain;
using ESchool.IdentityProvider.Grpc;
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

            services.AddDbContext<MasterDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MasterDbConnection"), config =>
                    config.MigrationsAssembly(typeof(ClassRegisterContext).Assembly.GetName().Name)));

            services.AddMassTransitOutbox(config =>
            {
                config.UseEntityFrameworkCore<ClassRegisterContext>(efCoreConfig =>
                {
                    efCoreConfig.UseMultiTenantMessageDispatcher();
                    efCoreConfig.UseTenantOutboxDbContextFactory<TenantDbContextFactory>();
                });
                config.AddPublishFilter(typeof(AuthDataSetterPublishFilter<>));
            });
            
            services.AddControllers();
            services.AddGrpc();

            services.AddMediatR(Assembly.Load("ESchool.ClassRegister.Application"))
                .AddMediatRAuthorization(Assembly.Load("ESchool.ClassRegister.Application"));
            
            services.AddAutoMapper(Assembly.Load("ESchool.ClassRegister.Application"));

            var authConfig = new AuthConfiguration();
            Configuration.GetSection("Authentication").Bind(authConfig);
            services.AddCommonAuthentication(authConfig);
            services.AddCommonAuthorization();

            services.AddOpenApiDocument(config =>
            {
                config.Title = "ESchool Class Register API";
                config.Description = "The REST API documentation of the Class Register microservice.";

                config.AddSecurity("OAuth2", new OpenApiSecurityScheme
                {
                    OpenIdConnectUrl = $"{authConfig.IdentityProviderUri}/.well-known/openid-configuration",
                    Scheme = "Bearer",
                    Type = OpenApiSecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = $"{authConfig.IdentityProviderUri}/connect/authorize",
                            TokenUrl = $"{authConfig.IdentityProviderUri}/connect/token",
                            Scopes = new Dictionary<string, string>
                            {
                                { "testingapi.readwrite", "testingapi.readwrite" },
                                { "classregisterapi.readwrite", "classregisterapi.readwrite" },
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

            services.AddMassTransit(config =>
            {
                config.AddConsumers(Assembly.Load("ESchool.ClassRegister.Application"));
                config.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(Configuration.GetValue<string>("RabbitMQ:Host"));
                    configurator.ReceiveEndpoint("class-register", endpoint =>
                    {
                        endpoint.ConfigureConsumers(context);
                    });

                    configurator.UseCustomFilters(context);
                });
            });
            services.AddMassTransitHostedService();

            services.AddCommonServices();
            services.AddMultitenancy();

            services.AddGrpcClient<TenantService.TenantServiceClient>(options =>
            {
                options.Address = new Uri(authConfig.IdentityProviderUri);
            });
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
                endpoints.MapGrpcService<LessonServiceImpl>();
            });
        }
    }
}
