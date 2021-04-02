using System;
using ESchool.Libs.Outbox.EntityFrameworkCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Libs.Outbox.EntityFrameworkCore
{
    public class OutboxDbContext : DbContext
    {
        public DbSet<OutboxEntry> OutboxEntries { get; set; }

        public OutboxDbContext(DbContextOptions<OutboxDbContext> options) : base(options)
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