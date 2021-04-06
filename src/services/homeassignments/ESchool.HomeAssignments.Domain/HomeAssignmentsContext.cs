using System.Linq;
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
using ESchool.Libs.Outbox.EntityFrameworkCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ESchool.HomeAssignments.Domain
{
    public class HomeAssignmentsContext : OutboxDbContext
    {
        private readonly Tenant tenant;
        private readonly IIdentityService identityService;

        public DbSet<ClassSchoolYearSubjectTeacher> ClassSchoolYearSubjectTeachers { get; set; }
        public DbSet<ClassSchoolYearSubjectStudent> ClassSchoolYearSubjectStudents { get; set; }
        public DbSet<ClassSchoolYearSubject> ClassSchoolYearSubjects { get; set; }
        public DbSet<HomeAssignmentsUserRole> UserRoles { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<HomeAssignmentsUser> Users { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<HomeworkSolution> HomeworkSolutions { get; set; }
        public DbSet<HomeworkReview> HomeworkReviews { get; set; }

        public HomeAssignmentsContext(
            DbContextOptions<HomeAssignmentsContext> options,
            Tenant tenant,
            IIdentityService identityService,
            IMediator mediator,
            ILogger<HomeAssignmentsContext> logger) : base(options, mediator, logger)
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
            
            var currentUserId = identityService.TryGetCurrentUserId();
            if (currentUserId != null)
            {
                await this.CreationAuditAsync<HomeAssignmentsUser, HomeAssignmentsUserRole>(currentUserId.Value, cancellationToken);
                await this.ModificationAuditAsync<HomeAssignmentsUser, HomeAssignmentsUserRole>(currentUserId.Value, cancellationToken);
            }
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            await EntityAuditAsync(cancellationToken);
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}