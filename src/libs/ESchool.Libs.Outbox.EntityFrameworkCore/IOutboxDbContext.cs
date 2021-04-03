using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Outbox.EntityFrameworkCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ESchool.Libs.Outbox.EntityFrameworkCore
{
    public interface IOutboxDbContext
    {
        IModel Model { get; }
        public DatabaseFacade Database { get; }
        public DbSet<OutboxEntry> OutboxEntries { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}