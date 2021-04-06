using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Outbox.EntityFrameworkCore.Commands;
using ESchool.Libs.Outbox.EntityFrameworkCore.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ESchool.Libs.Outbox.EntityFrameworkCore
{
    public abstract class OutboxDbContext : DbContext, IOutboxDbContext
    {
        private readonly IMediator mediator;
        private readonly ILogger logger;

        public DbSet<OutboxEntry> OutboxEntries { get; set; }

        protected OutboxDbContext(DbContextOptions options, IMediator mediator, ILogger logger) : base(options)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OutboxEntry>()
                .HasIndex(x => x.State)
                .IsUnique(false);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
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
    }
}