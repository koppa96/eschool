using ESchool.ClassRegister.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.ClassRegister.Domain.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<Teacher>,
        IEntityTypeConfiguration<Parent>,
        IEntityTypeConfiguration<Student>,
        IEntityTypeConfiguration<UserBase>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasBaseType<UserBase>();

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
            builder.HasBaseType<UserBase>();
            
            builder.HasMany(x => x.StudentParents)
                .WithOne(x => x.Parent)
                .HasForeignKey(x => x.ParentId);
        }

        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasBaseType<UserBase>();

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

        public void Configure(EntityTypeBuilder<UserBase> builder)
        {
            builder.HasMany(x => x.ReceivedMessages)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.SentMessages)
                .WithOne(x => x.SenderUser)
                .HasForeignKey(x => x.SenderUserId);
        }
    }
}