using ESchool.Libs.Domain.MultiTenancy.Entities;

namespace ESchool.Libs.Outbox.EntityFrameworkCore.Services
{
    public interface ITenantOutboxDbContextFactory
    {
        IOutboxDbContext CreateContext(Tenant tenant);
    }
}