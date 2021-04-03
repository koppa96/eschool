using ESchool.Libs.Outbox.EntityFrameworkCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Libs.Outbox.EntityFrameworkCore
{
    public abstract class OutboxDbContext : DbContext, IOutboxDbContext
    {
        public DbSet<OutboxEntry> OutboxEntries { get; set; }

        protected OutboxDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OutboxEntry>()
                .HasIndex(x => x.State)
                .IsUnique(false);
        }
    }
}