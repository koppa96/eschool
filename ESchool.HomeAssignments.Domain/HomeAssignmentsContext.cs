using System.Reflection;
using ESchool.HomeAssignments.Domain.Entities;
using ESchool.HomeAssignments.Domain.Entities.ClassRegisterData;
using ESchool.HomeAssignments.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Domain
{
    public class HomeAssignmentsContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<GroupStudent> GroupStudents { get; set; }
        public DbSet<GroupTeacher> GroupTeachers { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<HomeWork> HomeWorks { get; set; }
        public DbSet<HomeWorkSolution> HomeWorkSolutions { get; set; }
        public DbSet<HomeWorkReview> HomeWorkReviews { get; set; }
        
        public HomeAssignmentsContext(DbContextOptions<HomeAssignmentsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}