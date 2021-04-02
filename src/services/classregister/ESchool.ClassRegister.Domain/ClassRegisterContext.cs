using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.ClassRegister.Domain.Entities.Grading;
using ESchool.ClassRegister.Domain.Entities.Messaging;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.ClassRegister.Domain.Entities.Users;
using ESchool.ClassRegister.Domain.Entities.Users.Abstractions;
using ESchool.Libs.Domain.Extensions;
using ESchool.Libs.Domain.Interfaces;
using ESchool.Libs.Domain.MultiTenancy.Entities;
using ESchool.Libs.Outbox.EntityFrameworkCore;
using ESchool.Libs.Outbox.EntityFrameworkCore.Extensions;
using ESchool.Libs.Outbox.Services;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Domain
{
    public class ClassRegisterContext : DbContext
    {
        private readonly Tenant tenant;
        private readonly OutboxDbContext outboxDbContext;

        public DbSet<Class> Classes { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<ClassType> ClassTypes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SchoolYear> SchoolYears { get; set; }
        public DbSet<ClassSchoolYear> ClassSchoolYears { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<GradeKind> GradeKinds { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }
        public DbSet<Absence> Absences { get; set; }
        public DbSet<ClassSchoolYearSubject> ClassSchoolYearSubjects { get; set; }
        public DbSet<ClassSchoolYearSubjectTeacher> GroupTeachers { get; set; }
        public DbSet<HomeWork> HomeWorks { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<SubjectTeacher> SubjectTeachers { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<ClassRegisterUserRole> UserRoles { get; set; }
        public DbSet<ClassRegisterUser> Users { get; set; }
        public DbSet<ClassSchoolYearSubjectTeacher> ClassSchoolYearSubjectTeachers { get; set; }

        public ClassRegisterContext(
            DbContextOptions<ClassRegisterContext> options,
            Tenant tenant,
            OutboxDbContext outboxDbContext) : base(options)
        {
            this.tenant = tenant;
            this.outboxDbContext = outboxDbContext;
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

            modelBuilder.AddGlobalQueryFilter<ISoftDelete>(x => x.IsDeleted);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            EntityAudit();
            return this.SaveChangesWithOutbox(outboxDbContext, () => base.SaveChanges(acceptAllChangesOnSuccess));
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            EntityAudit();
            return this.SaveChangesWithOutboxAsync(
                outboxDbContext,
                token => base.SaveChangesAsync(acceptAllChangesOnSuccess, token),
                cancellationToken: cancellationToken);
        }

        private void EntityAudit()
        {
            this.SoftDelete();
        }
    }
}