using ESchool.HomeAssignments.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.HomeAssignments.Domain.EntityConfiguration
{
    public class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            builder.HasMany(x => x.StudentHomeworks)
                .WithOne(x => x.Homework)
                .HasForeignKey(x => x.HomeworkId);

            builder.HasMany(x => x.TeacherHomeworks)
                .WithOne(x => x.Homework)
                .HasForeignKey(x => x.HomeworkId);

            builder.HasMany(x => x.Solutions)
                .WithOne(x => x.Homework)
                .HasForeignKey(x => x.HomeworkId);

            builder.HasOne(x => x.Lesson)
                .WithMany(x => x.HomeWorks)
                .HasForeignKey(x => x.LessonId);
        }
    }
}