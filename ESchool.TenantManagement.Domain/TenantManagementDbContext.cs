using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.TenantManagement.Domain
{
    public class TenantManagementDbContext : DbContext
    {
        public DbSet<Tenant> Tenants { get; set; }

        public TenantManagementDbContext(DbContextOptions<TenantManagementDbContext> options) : base(options)
        {
        }
    }
}
