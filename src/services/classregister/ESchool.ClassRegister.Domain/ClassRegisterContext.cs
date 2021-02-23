using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.ClassRegister.Domain.Entities.Grading;
using ESchool.ClassRegister.Domain.Entities.Messaging;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.ClassRegister.Domain.Entities.Users;
using ESchool.Libs.Domain.Extensions;
using ESchool.Libs.Domain.Interfaces;
using ESchool.Libs.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Domain
{
    public class ClassRegisterContext : DbContext
    {
        private readonly Guid tenantId;

        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassRoom> ClassRooms { get; set; }
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
        public DbSet<UserBase> UserBases { get; set; }

        public ClassRegisterContext(IIdentityService identityService, DbContextOptions<ClassRegisterContext> options) :
            base(options)
        {
            tenantId = identityService.GetTenantId();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.AddGlobalQueryFilter<IMultiTenantEntity>(x => x.TenantId == tenantId);
            modelBuilder.AddGlobalQueryFilter<ISoftDelete>(x => x.IsDeleted);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            EntityAudit();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            EntityAudit();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void EntityAudit()
        {
            this.SetTenantId(tenantId);
            this.SoftDelete();
        }
    }
}