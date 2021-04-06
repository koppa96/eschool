using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using ESchool.IdentityProvider.Domain.Entities;
using ESchool.IdentityProvider.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using ESchool.Libs.Domain.Services;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Extensions;
using ESchool.Libs.Domain.Interfaces;
using ESchool.Libs.Outbox.EntityFrameworkCore;
using ESchool.Libs.Outbox.EntityFrameworkCore.Commands;
using ESchool.Libs.Outbox.EntityFrameworkCore.Entities;
using ESchool.Libs.Outbox.EntityFrameworkCore.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ESchool.IdentityProvider.Domain
{
    public class IdentityProviderContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>, IOutboxDbContext
    {
        private readonly IMediator mediator;
        private readonly ILogger<IdentityProviderContext> logger;
        private readonly Guid? tenantId;

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantUser> TenantUsers { get; set; }
        public DbSet<TenantUserRole> TenantUserRoles { get; set; }
        public DbSet<OutboxEntry> OutboxEntries { get; set; }
        
        public IdentityProviderContext(
            DbContextOptions<IdentityProviderContext> options,
            IIdentityService identityService,
            IMediator mediator,
            ILogger<IdentityProviderContext> logger) : base(options)
        {
            this.mediator = mediator;
            this.logger = logger;
            tenantId = identityService.TryGetTenantId();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<OutboxEntry>()
                .HasIndex(x => x.State)
                .IsUnique(false);
        }
        
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            EntityAudit();
            var addedEntries = ChangeTracker.Entries<OutboxEntry>()
                .Where(x => x.State == EntityState.Added)
                .ToList();

            var result = base.SaveChanges(acceptAllChangesOnSuccess);
            if (!addedEntries.Any())
            {
                return result;
            }

            try
            {
                mediator.Send(new DispatchSavedMessagesCommand
                {
                    MessageIds = addedEntries.Select(x => x.Entity.Id)
                }).GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to immediately dispatch the messages");
            }
            
            return result;
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            EntityAudit();
            var addedEntries = ChangeTracker.Entries<OutboxEntry>()
                .Where(x => x.State == EntityState.Added)
                .ToList();

            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            if (!addedEntries.Any())
            {
                return result;
            }

            try
            {
                // IMessageDispatchert nem injektálhatunk közvetlenül, mert annak kell egy IOutboxDbContext ha nem multitenant
                // A mediátor akkor fogja csak feloldani a handlert, és ezzel együtt a dispatchert amikor entryk lettek hozzáadva
                await mediator.Send(new DispatchSavedMessagesCommand
                {
                    MessageIds = addedEntries.Select(x => x.Entity.Id)
                }, cancellationToken);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to immediately dispatch the messages");
            }
            
            return result;
        }

        private void EntityAudit()
        {
            // TODO: Implement auditing methods
        }
    }
}
