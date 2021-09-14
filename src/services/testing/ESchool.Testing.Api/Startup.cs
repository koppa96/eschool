using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json.Polymorph.Extensions;
using System.Text.Json.Serialization;
using ESchool.ClassRegister.Grpc;
using ESchool.IdentityProvider.Interface.DefaultHandlers.Extensions;
using ESchool.Libs.AspNetCore.Configuration;
using ESchool.Libs.AspNetCore.Extensions;
using ESchool.Libs.AspNetCore.Filters;
using ESchool.Libs.AspNetCore.Middlewares;
using ESchool.Libs.Domain.MultiTenancy;
using ESchool.Libs.Outbox.AspNetCore.Extensions;
using ESchool.Libs.Outbox.EntityFrameworkCore.Extensions;
using ESchool.Testing.Domain;
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

namespace ESchool.Testing.Api
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
            services.AddDbContext<MasterDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("MasterDbConnection"), serverConfig =>
                    serverConfig.MigrationsAssembly(typeof(TestingContext).Assembly.GetName().Name)));

            services.AddDbContext<TestingContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddLazyDbContext<TestingContext>();

            services.AddMultitenancy<TestingContext>();

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.AddDiscriminatorConverters(Assembly.Load("ESchool.Testing.Application"));
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: false));
                });

            var authConfig = new AuthConfiguration();
            Configuration.GetSection("Authentication").Bind(authConfig);
            services.AddCommonAuthentication(authConfig);
            services.AddCommonAuthorization();

            services.AddMediatR(Assembly.Load("ESchool.Testing.Application"));
            
            var rabbitMqConfig = new RabbitMqConfiguration();
            Configuration.GetSection("RabbitMQ").Bind(rabbitMqConfig);
            services.AddMassTransit(config =>
            {
                config.AddConsumers(Assembly.Load("ESchool.Testing.Application"));
                config.AddTenantEventConsumers<TestingContext>();
                config.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(rabbitMqConfig.Host, rabbitConfig =>
                    {
                        rabbitConfig.Username(rabbitMqConfig.Username);
                        rabbitConfig.Password(rabbitMqConfig.Password);
                    });
                    configurator.ReceiveEndpoint("testing", endpoint => { endpoint.ConfigureConsumers(context); });
                });
            });
            services.AddMassTransitHostedService();
            services.AddMassTransitOutbox(options =>
            {
                options.UseEntityFrameworkCore<TestingContext>(config => { config.UseMultiTenantMessageDispatcher(); });
                options.AddPublishFilter<AuthDataSetterPublishFilter>();
            });
            
            services.AddOpenApiDocument(config =>
            {
                config.Title = "ESchool Class Register API";
                config.Description = "The REST API documentation of the Class Register microservice.";
#pragma warning disable 618
                config.DefaultEnumHandling = EnumHandling.String;
#pragma warning restore 618

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
            });

            services.AddAutoMapper(Assembly.Load("ESchool.Testing.Application"));
            services.AddCommonServices();

            services.AddGrpcClient<ClassSchoolYearSubjectService.ClassSchoolYearSubjectServiceClient>(options =>
            {
                options.Address = new Uri(Configuration.GetValue<string>("ClassRegisterUri"));
            });

            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<RequestLoggerMiddleware>();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
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

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}