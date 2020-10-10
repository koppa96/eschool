using System.Reflection;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.ClassRegister.Domain.Entities.Grading;
using ESchool.ClassRegister.Domain.Entities.Messaging;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Domain
{
    public class ClassRegisterContext : DbContext
    {
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassRoom> ClassRooms { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SchoolYear> SchoolYears { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<GradeKind> GradeKinds { get; set; }
        public DbSet<SmallGrade> SmallGrades { get; set; }
        public DbSet<SmallGradesPolicy> SmallGradesPolicies { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }
        public DbSet<Absence> Absences { get; set; }
        public DbSet<ClassSubject> ClassSubjects { get; set; }
        public DbSet<ClassSubjectGroup> ClassSubjectGroups { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupStudent> GroupStudents { get; set; }
        public DbSet<GroupTeacher> GroupTeachers { get; set; }
        public DbSet<HomeWork> HomeWorks { get; set; }
        public DbSet<Lesson> Lessons { get; set; }

        public ClassRegisterContext(DbContextOptions<ClassRegisterContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}