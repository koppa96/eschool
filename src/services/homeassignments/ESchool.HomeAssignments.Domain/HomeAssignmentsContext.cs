using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.HomeAssignments.Domain.Entities.ClassRegisterData;
using ESchool.HomeAssignments.Domain.Entities.Users;
using ESchool.Libs.Domain.Extensions;
using ESchool.Libs.Domain.Interfaces;
using ESchool.Libs.Domain.MultiTenancy.Entities;
using ESchool.Libs.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Domain
{
    public class HomeAssignmentsContext : DbContext
    {
        private readonly Tenant tenant;
        private readonly IIdentityService identityService;
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<HomeworkSolution> HomeworkSolutions { get; set; }
        public DbSet<HomeworkReview> HomeworkReviews { get; set; }
        public DbSet<StudentHomework> StudentHomeworks { get; set; }
        public DbSet<TeacherHomework> TeacherHomeworks { get; set; }
        
        public HomeAssignmentsContext(DbContextOptions<HomeAssignmentsContext> options, Tenant tenant, IIdentityService identityService) : base(options)
        {
            this.tenant = tenant;
            this.identityService = identityService;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (tenant != null)
            {
                optionsBuilder.UseSqlServer(tenant.DbConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.AddGlobalQueryFilter<ISoftDelete>(x => !x.IsDeleted);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            EntityAuditAsync().GetAwaiter().GetResult();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        private async Task EntityAuditAsync(CancellationToken cancellationToken = default)
        {
            this.SoftDelete();
            await this.CreationAuditAsync(identityService.GetCurrentUserId(), cancellationToken);
            await this.ModificationAuditAsync(identityService.GetCurrentUserId(), cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            await EntityAuditAsync(cancellationToken);
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}