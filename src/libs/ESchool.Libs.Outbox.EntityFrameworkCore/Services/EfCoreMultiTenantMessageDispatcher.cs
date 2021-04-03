using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Domain.MultiTenancy;
using ESchool.Libs.Domain.MultiTenancy.Entities;
using ESchool.Libs.Outbox.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ESchool.Libs.Outbox.EntityFrameworkCore.Services
{
    public class EfCoreMultiTenantMessageDispatcher : IMessageDispatcher
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ITenantOutboxDbContextFactory tenantOutboxDbContextFactory;
        private readonly MasterDbContext masterDbContext;

        public EfCoreMultiTenantMessageDispatcher(IServiceProvider serviceProvider,
            ITenantOutboxDbContextFactory tenantOutboxDbContextFactory,
            MasterDbContext masterDbContext)
        {
            this.serviceProvider = serviceProvider;
            this.tenantOutboxDbContextFactory = tenantOutboxDbContextFactory;
            this.masterDbContext = masterDbContext;
        }
        
        public async Task DispatchAllAsync(CancellationToken cancellationToken = default)
        {
            // Ez a metódus a bg service által kerül meghívásra, tenant nem elérhető
            // => Dispatchelünk minden tenant outboxából
            var tenants = await masterDbContext.Tenants.ToListAsync(cancellationToken);
            foreach (var tenant in tenants)
            {
                await using var context = tenantOutboxDbContextFactory.CreateContext(tenant);
                var dispatcher = new EfCoreMessageDispatcher(
                    context,
                    serviceProvider.GetRequiredService<IOptions<OutboxConfiguration>>(),
                    serviceProvider.GetRequiredService<IPublishEndpoint>(),
                    serviceProvider.GetRequiredService<ILogger<EfCoreMessageDispatcher>>());

                await dispatcher.DispatchAllAsync(cancellationToken);
            }
        }

        public async Task DispatchMessagesAsync(IEnumerable<Guid> messageIds, CancellationToken cancellationToken = default)
        {
            // Ez a metódus a middleware vagy a MT filter által kerül meghívásra, van tenantunk
            // => Dispatcheljük az adott azonosítójú üzeneteket a tenantból
            var tenant = serviceProvider.GetRequiredService<Tenant>();
            await using var context = tenantOutboxDbContextFactory.CreateContext(tenant);
            
            var dispatcher = new EfCoreMessageDispatcher(
                context,
                serviceProvider.GetRequiredService<IOptions<OutboxConfiguration>>(),
                serviceProvider.GetRequiredService<IPublishEndpoint>(),
                serviceProvider.GetRequiredService<ILogger<EfCoreMessageDispatcher>>());

            await dispatcher.DispatchMessagesAsync(messageIds, cancellationToken);
        }
    }
}