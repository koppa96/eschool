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
            Func<int> baseSaveChanges)
        {
            var result = 0;
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                result = baseSaveChanges();
                outboxDbContext.SaveChanges();
                scope.Complete();
            }

            return result;
        }

        public static async Task<int> SaveChangesWithOutboxAsync(
            this DbContext dbContext,
            OutboxDbContext outboxDbContext,
            Func<CancellationToken, Task<int>> baseSaveChangesAsync,
            CancellationToken cancellationToken = default)
        {
            var result = 0;
            using (var scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await baseSaveChangesAsync(cancellationToken);
                await outboxDbContext.SaveChangesAsync(cancellationToken);
                scope.Complete();
            }
            
            return result;
        }
    }
}