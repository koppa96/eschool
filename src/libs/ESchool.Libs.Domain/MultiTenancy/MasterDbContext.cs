using ESchool.Libs.Domain.MultiTenancy.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Libs.Domain.MultiTenancy
{
    public class MasterDbContext : DbContext
    {
        public DbSet<Tenant> Tenants { get; set; }

        public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options)
        {
        }
    }
}