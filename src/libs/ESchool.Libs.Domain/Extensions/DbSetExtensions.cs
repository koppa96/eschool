using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Libs.Domain.Extensions
{
    public static class DbSetExtensions
    {
        public static async Task<T> FindOrThrowAsync<T>(this DbSet<T> dbSet, object key, CancellationToken cancellationToken = default)
            where T : class
        {
            var result = await dbSet.FindAsync(new object[] { key }, cancellationToken);
            return result ?? throw new InvalidOperationException($"No entity of type {typeof(T).FullName} was found.");
        }
    }
}