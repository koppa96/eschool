using ESchool.HomeAssignments.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.HomeAssignments.Domain.EntityConfiguration
{
    public class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            builder.HasOne(x => x.Lesson)
                .WithMany(x => x.HomeWorks)
                .HasForeignKey(x => x.LessonId);
        }
    }
}