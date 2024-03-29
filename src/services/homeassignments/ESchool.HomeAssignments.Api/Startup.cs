using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using ESchool.ClassRegister.Grpc;
using ESchool.HomeAssignments.Api.Infrastructure;
using ESchool.HomeAssignments.Api.Infrastructure.Configuration;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Services;
using ESchool.IdentityProvider.Interface.DefaultHandlers.Extensions;
using ESchool.Libs.AspNetCore.Configuration;
using ESchool.Libs.AspNetCore.Extensions;
using ESchool.Libs.AspNetCore.Filters;
using ESchool.Libs.AspNetCore.Middlewares;
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
using NJsonSchema.Generation;
using NSwag;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;

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
            services.Configure<LocalSolutionFileHandlerConfig>(
                Configuration.GetSection("LocalSolutionFileHandlerConfig"));
            
            services.AddMemoryCache();
            services.AddDbContext<HomeAssignmentsContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddLazyDbContext<HomeAssignmentsContext>();
            
            services.AddDbContext<MasterDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("MasterDbConnection"), config =>
                    config.MigrationsAssembly(typeof(HomeAssignmentsContext).Assembly.GetName().Name)));

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: false));
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });

            var authConfig = new AuthConfiguration();
            Configuration.GetSection("Authentication").Bind(authConfig);
            services.AddCommonAuthentication(authConfig);
            services.AddCommonAuthorization();

            services.AddCommonServices();
            services.AddMultitenancy<HomeAssignmentsContext>();

            services.AddMediatR(Assembly.Load("ESchool.HomeAssignments.Application"))
                .AddMediatRAuthorization(Assembly.Load("ESchool.HomeAssignments.Application"));

            var rabbitMqConfig = new RabbitMqConfiguration();
            Configuration.GetSection("RabbitMQ").Bind(rabbitMqConfig);
            services.AddMassTransit(config =>
            {
                config.AddConsumers(Assembly.Load("ESchool.HomeAssignments.Application"));
                config.AddTenantEventConsumers<HomeAssignmentsContext>();
                config.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(rabbitMqConfig.Host, rabbitConfig =>
                    {
                        rabbitConfig.Username(rabbitMqConfig.Username);
                        rabbitConfig.Password(rabbitMqConfig.Password);
                    });
                    configurator.ReceiveEndpoint("home-assignments", endpoint =>
                    {
                        endpoint.ConfigureConsumers(context);
                    });
                    
                    configurator.UseCustomFilters(context);
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
            
            services.AddOpenApiDocument(config =>
            {
                config.Title = "ESchool Home Assigments API";
                config.Description = "The REST API documentation of the Home Assignments microservice.";

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

            services.AddMultitenancy<HomeAssignmentsContext>();
            services.AddTransient<ISolutionFileHandlerService, LocalSolutionFileHandlerService>();
            
            services.AddAutoMapper(Assembly.Load("ESchool.HomeAssignments.Application"));
            
            services.AddGrpcClient<ClassSchoolYearSubjectService.ClassSchoolYearSubjectServiceClient>(config =>
            {
                config.Address = new Uri(Configuration.GetValue<string>("ClassRegisterUri"));
            });
            
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
            app.UseExceptionHandlerMiddleware();
            app.UseMiddleware<RequestLoggerMiddleware>();

            app.UseCors();
            app.UseOpenApi();
            app.UseSwaggerUi3(config =>
            {
                config.DocumentPath = Configuration.GetValue<string>("Swagger:DocumentPath");
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
