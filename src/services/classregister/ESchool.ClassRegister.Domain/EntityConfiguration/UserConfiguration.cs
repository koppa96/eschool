using ESchool.ClassRegister.Domain.Entities.Users;
using ESchool.ClassRegister.Domain.Entities.Users.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.ClassRegister.Domain.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<Teacher>,
        IEntityTypeConfiguration<Parent>,
        IEntityTypeConfiguration<Student>,
        IEntityTypeConfiguration<Administrator>,
        IEntityTypeConfiguration<ClassRegisterUser>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasBaseType<ClassRegisterUserRole>();

            builder.HasMany(x => x.ClassSchoolYearSubjectTeachers)
                .WithOne(x => x.Teacher)
                .HasForeignKey(x => x.TeacherId);

            builder.HasMany(x => x.PreviousClasses)
                .WithOne(x => x.HeadTeacher)
                .HasForeignKey(x => x.HeadTeacherId);

            builder.HasOne(x => x.CurrentClass)
                .WithOne()
                .HasForeignKey<Teacher>(x => x.CurrentClassId);
        }

        public void Configure(EntityTypeBuilder<Parent> builder)
        {
            builder.HasBaseType<ClassRegisterUserRole>();
            
            builder.HasMany(x => x.StudentParents)
                .WithOne(x => x.Parent)
                .HasForeignKey(x => x.ParentId);
        }

        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasBaseType<ClassRegisterUserRole>();

            builder.HasMany(x => x.StudentParents)
                .WithOne(x => x.Student)
                .HasForeignKey(x => x.StudentId);

            builder.HasMany(x => x.Grades)
                .WithOne(x => x.Student)
                .HasForeignKey(x => x.StudentId);

            builder.HasMany(x => x.Absences)
                .WithOne(x => x.Student)
                .HasForeignKey(x => x.StudentId);

            builder.HasOne(x => x.Class)
                .WithMany(x => x.Students)
                .HasForeignKey(x => x.ClassId);
        }

        public void Configure(EntityTypeBuilder<ClassRegisterUser> builder)
        {
            builder.HasMany(x => x.UserRoles)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }

        public void Configure(EntityTypeBuilder<Administrator> builder)
        {
            builder.HasBaseType<ClassRegisterUserRole>();
        }
    }
}