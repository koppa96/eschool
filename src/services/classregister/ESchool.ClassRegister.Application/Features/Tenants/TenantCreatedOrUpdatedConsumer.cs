﻿using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.IdentityProvider.Grpc;
using ESchool.IdentityProvider.Interface.IntegrationEvents.Tenants;
using ESchool.Libs.Domain.MultiTenancy;
using ESchool.Libs.Domain.MultiTenancy.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace ESchool.ClassRegister.Application.Features.Tenants
{
    public class TenantCreatedOrUpdatedConsumer : IConsumer<TenantCreatedOrUpdatedEvent>
    {
        private readonly MasterDbContext masterDbContext;
        private readonly TenantService.TenantServiceClient client;
        private readonly IConfiguration configuration;
        private readonly ITenantDbContextFactory<ClassRegisterContext> tenantDbContextFactory;
        private readonly IMemoryCache memoryCache;

        public TenantCreatedOrUpdatedConsumer(MasterDbContext masterDbContext,
            TenantService.TenantServiceClient client,
            IConfiguration configuration,
            ITenantDbContextFactory<ClassRegisterContext> tenantDbContextFactory,
            IMemoryCache memoryCache)
        {
            this.masterDbContext = masterDbContext;
            this.client = client;
            this.configuration = configuration;
            this.tenantDbContextFactory = tenantDbContextFactory;
            this.memoryCache = memoryCache;
        }
        
        public async Task Consume(ConsumeContext<TenantCreatedOrUpdatedEvent> context)
        {
            var tenant = await masterDbContext.Tenants.FindAsync(context.Message.TenantId);
            if (tenant == null)
            {
                tenant = new Tenant
                {
                    Id = context.Message.TenantId,
                    DbConnectionString = string.Format(configuration.GetConnectionString("ConnectionStringTemplate"),
                        context.Message.TenantId)
                };
                masterDbContext.Tenants.Add(tenant);

                await using var tenantDbContext = tenantDbContextFactory.CreateContext(tenant);
                await tenantDbContext.Database.MigrateAsync();
            }

            var remoteTenant = await client.GetTenantDetailsAsync(new TenantDetailsRequest
            {
                TenantId = context.Message.TenantId.ToString()
            });

            tenant.Name = remoteTenant.Name;
            tenant.OmIdentifier = remoteTenant.OmIdentifier;

            memoryCache.Set(tenant.Id, tenant);
            await masterDbContext.SaveChangesAsync();
        }
    }
}