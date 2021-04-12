using ESchool.Testing.Domain.Entities;
using ESchool.Testing.Domain.Entities.Answers;
using ESchool.Testing.Domain.Entities.ClassRegisterData;
using ESchool.Testing.Domain.Entities.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ESchool.Libs.Domain.MultiTenancy.Entities;
using ESchool.Libs.Outbox.EntityFrameworkCore;
using ESchool.Testing.Domain.Entities.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ESchool.Testing.Domain
{
    public class TestingContext : OutboxDbContext
    {
        private readonly Tenant tenant;
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TestingUser> Users { get; set; }
        public DbSet<TestingUserRole> UserRoles { get; set; }
        public DbSet<ClassSchoolYearSubject> Groups { get; set; }
        public DbSet<StudentTest> TestGroups { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestAnswer> Answers { get; set; }
        public DbSet<TestTask> Tasks { get; set; }
        public DbSet<TaskAnswer> TaskAnswers { get; set; }

        public TestingContext(DbContextOptions<TestingContext> options,
            IMediator mediator,
            ILogger<TestingContext> logger,
            Tenant tenant) : base(options, mediator, logger)
        {
            this.tenant = tenant;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (tenant != null)
            {
                optionsBuilder.UseSqlServer(tenant.DbConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
