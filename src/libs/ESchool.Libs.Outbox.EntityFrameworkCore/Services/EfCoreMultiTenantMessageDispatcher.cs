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
    public class EfCoreMultiTenantMessageDispatcher<TContext> : IMessageDispatcher
        where TContext : DbContext, IOutboxDbContext
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ITenantDbContextFactory<TContext> tenantOutboxDbContextFactory;
        private readonly MasterDbContext masterDbContext;

        public EfCoreMultiTenantMessageDispatcher(IServiceProvider serviceProvider,
            ITenantDbContextFactory<TContext> tenantOutboxDbContextFactory,
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
                var dispatcher = new EfCoreMessageDispatcher<TContext>(
                    context,
                    serviceProvider.GetRequiredService<IOptions<OutboxConfiguration>>(),
                    serviceProvider.GetRequiredService<IPublishEndpoint>(),
                    serviceProvider.GetRequiredService<ILogger<EfCoreMessageDispatcher<TContext>>>());

                await dispatcher.DispatchAllAsync(cancellationToken);
            }
        }

        public async Task TryDispatchMessagesAsync(IEnumerable<Guid> messageIds,
            CancellationToken cancellationToken = default)
        {
            // Ez a metódus a middleware vagy a MT filter által kerül meghívásra, nem biztos, hogy van tenantunk
            // => Ha van, akkor dispatcheljük az adott azonosítójú üzeneteket a tenantból
            // => Ha nincs akkor kivárjuk hogy a background job mindent dispatcheljen
            var tenant = serviceProvider.GetService<Tenant>();
            if (tenant != null)
            {
                await using var context = tenantOutboxDbContextFactory.CreateContext(tenant);

                var dispatcher = new EfCoreMessageDispatcher<TContext>(
                    context,
                    serviceProvider.GetRequiredService<IOptions<OutboxConfiguration>>(),
                    serviceProvider.GetRequiredService<IPublishEndpoint>(),
                    serviceProvider.GetRequiredService<ILogger<EfCoreMessageDispatcher<TContext>>>());

                await dispatcher.TryDispatchMessagesAsync(messageIds, cancellationToken);
            }
        }
    }
}