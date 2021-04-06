using ESchool.Libs.Domain.MultiTenancy.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Libs.Domain.MultiTenancy
{
    /// <summary>
    /// Creates a DbContext instance for the specified tenant.
    /// </summary>
    /// <typeparam name="TContext">The type of the DbContext the factory returns</typeparam>
    public interface ITenantDbContextFactory<out TContext>
        where TContext : DbContext
    {
        /// <summary>
        /// Creates an instance of the <typeparamref name="TContext"/> for the specified tenant.
        /// </summary>
        /// <param name="tenant">The tenant of the DbContext instance</param>
        /// <returns>An instance of DbContext</returns>
        TContext CreateContext(Tenant tenant);
    }
}