using ESchool.ClassRegister.Domain;
using ESchool.Libs.Domain.MultiTenancy.Entities;
using ESchool.Libs.Outbox.EntityFrameworkCore;
using ESchool.Libs.Outbox.EntityFrameworkCore.Services;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Api.Infrastructure
{
    public class TenantDbContextFactory : ITenantOutboxDbContextFactory
    {
        private readonly DbContextOptions<ClassRegisterContext> dbContextOptions;


        public TenantDbContextFactory(DbContextOptions<ClassRegisterContext> dbContextOptions)
        {
            this.dbContextOptions = dbContextOptions;
        }
        
        public IOutboxDbContext CreateContext(Tenant tenant)
        {
            return new ClassRegisterContext(dbContextOptions, tenant);
        }
    }
}