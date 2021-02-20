using ESchool.Testing.Domain.Entities;
using ESchool.Testing.Domain.Entities.Answers;
using ESchool.Testing.Domain.Entities.ClassRegisterData;
using ESchool.Testing.Domain.Entities.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ESchool.Testing.Domain
{
    public class TestingContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<TestGroup> TestGroups { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestAnswer> Answers { get; set; }
        public DbSet<TestTask> Tasks { get; set; }
        public DbSet<TaskAnswer> TaskAnswers { get; set; }

        public TestingContext(DbContextOptions<TestingContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
