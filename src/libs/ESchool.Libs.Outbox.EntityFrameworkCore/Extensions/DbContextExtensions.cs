using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Libs.Outbox.EntityFrameworkCore.Extensions
{
    public static class DbContextExtensions
    {
        public static int SaveChangesWithOutbox(
            this DbContext dbContext,
            OutboxDbContext outboxDbContext,
            Func<int> baseSaveChanges,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            using var transaction = new CommittableTransaction(new TransactionOptions
            {
                IsolationLevel = isolationLevel
            });
            
            dbContext.Database.OpenConnection();
            outboxDbContext.Database.OpenConnection();
            
            dbContext.Database.EnlistTransaction(transaction);
            outboxDbContext.Database.EnlistTransaction(transaction);

            var result = baseSaveChanges();
            outboxDbContext.SaveChanges();
            transaction.Commit();

            dbContext.Database.CloseConnection();
            outboxDbContext.Database.CloseConnection();
            
            return result;
        }

        public static async Task<int> SaveChangesWithOutboxAsync(
            this DbContext dbContext,
            OutboxDbContext outboxDbContext,
            Func<CancellationToken, Task<int>> baseSaveChangesAsync,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
            CancellationToken cancellationToken = default)
        {
            using var transaction = new CommittableTransaction(new TransactionOptions
            {
                IsolationLevel = isolationLevel
            });

            await dbContext.Database.OpenConnectionAsync(cancellationToken);
            await outboxDbContext.Database.OpenConnectionAsync(cancellationToken);
            
            dbContext.Database.EnlistTransaction(transaction);
            outboxDbContext.Database.EnlistTransaction(transaction);

            var result = await baseSaveChangesAsync(cancellationToken);
            await outboxDbContext.SaveChangesAsync(cancellationToken);
            transaction.Commit();

            await dbContext.Database.CloseConnectionAsync();
            await outboxDbContext.Database.CloseConnectionAsync();

            return result;
        }
    }
}