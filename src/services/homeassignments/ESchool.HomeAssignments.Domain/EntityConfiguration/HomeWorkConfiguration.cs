using ESchool.HomeAssignments.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.HomeAssignments.Domain.EntityConfiguration
{
    public class HomeWorkConfiguration : IEntityTypeConfiguration<HomeWork>
    {
        public void Configure(EntityTypeBuilder<HomeWork> builder)
        {
            builder.HasOne(x => x.Lesson)
                .WithMany(x => x.HomeWorks)
                .HasForeignKey(x => x.LessonId);

            builder.HasMany(x => x.Solutions)
                .WithOne(x => x.HomeWork)
                .HasForeignKey(x => x.HomeWorkId);
            
        }
    }
}