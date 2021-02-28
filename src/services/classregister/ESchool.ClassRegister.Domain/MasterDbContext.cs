using ESchool.ClassRegister.Domain.Entities.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Domain
{
    public class MasterDbContext : DbContext
    {
        public DbSet<Tenant> Tenants { get; set; }

        public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options)
        {
        }
    }
}