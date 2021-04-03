using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Outbox.EntityFrameworkCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ESchool.Libs.Outbox.EntityFrameworkCore
{
    public interface IOutboxDbContext : IDisposable, IAsyncDisposable
    {
        IModel Model { get; }
        DatabaseFacade Database { get; }
        DbSet<OutboxEntry> OutboxEntries { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}