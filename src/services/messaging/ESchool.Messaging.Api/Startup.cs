using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using ESchool.IdentityProvider.Interface.DefaultHandlers.Extensions;
using ESchool.Libs.AspNetCore.Configuration;
using ESchool.Libs.AspNetCore.Extensions;
using ESchool.Libs.AspNetCore.Filters;
using ESchool.Libs.AspNetCore.Middlewares;
using ESchool.Libs.Domain.MultiTenancy;
using ESchool.Libs.Outbox;
using ESchool.Libs.Outbox.AspNetCore.Extensions;
using ESchool.Libs.Outbox.EntityFrameworkCore.Extensions;
using ESchool.Messaging.Domain;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;

namespace ESchool.Messaging.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddDbContext<MessagingContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddLazyDbContext<MessagingContext>();

            services.AddDbContext<MasterDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("MasterDbConnection"), config =>
                    config.MigrationsAssembly(typeof(MessagingContext).Assembly.GetName().Name)));

            services.Configure<OutboxConfiguration>(Configuration.GetSection("Outbox"));
            services.AddMassTransitOutbox(config =>
            {
                config.UseEntityFrameworkCore<MessagingContext>(efCoreConfig =>
                {
                    efCoreConfig.UseMultiTenantMessageDispatcher();
                });
                config.AddPublishFilter<AuthDataSetterPublishFilter>();
            });
            
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: false));
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });
            
            services.AddMediatR(Assembly.Load("ESchool.Messaging.Application"))
                .AddMediatRAuthorization(Assembly.Load("ESchool.Messaging.Application"));
            
            services.AddAutoMapper(Assembly.Load("ESchool.Messaging.Application"));

            var authConfig = new AuthConfiguration();
            Configuration.GetSection("Authentication").Bind(authConfig);
            services.AddCommonAuthentication(authConfig);
            services.AddCommonAuthorization();

            services.AddOpenApiDocument(config =>
            {
                config.Title = "ESchool Messaging API";
                config.Description = "The REST API documentation of the Messaging microservice.";

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
                                { "messagingapi.readwrite", "messagingapi.readwrite" },
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
            services.AddMassTransit(config =>
            {
                config.AddConsumers(Assembly.Load("ESchool.Messaging.Application"));
                config.AddTenantEventConsumers<MessagingContext>();
                config.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(rabbitMqConfig.Host, rabbitConfig =>
                    {
                        rabbitConfig.Username(rabbitMqConfig.Username);
                        rabbitConfig.Password(rabbitMqConfig.Password);
                    });
                    configurator.ReceiveEndpoint("class-register", endpoint =>
                    {
                        endpoint.ConfigureConsumers(context);
                    });

                    configurator.UseCustomFilters(context);
                });
            });
            services.AddMassTransitHostedService();

            services.AddCommonServices();
            services.AddMultitenancy<MessagingContext>();
            
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