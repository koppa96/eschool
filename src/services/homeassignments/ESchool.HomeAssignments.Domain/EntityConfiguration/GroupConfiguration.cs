using ESchool.HomeAssignments.Domain.Entities.ClassRegisterData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.HomeAssignments.Domain.EntityConfiguration
{
    public class GroupConfiguration : IEntityTypeConfiguration<HomeWorkGroup>
    {
        public void Configure(EntityTypeBuilder<HomeWorkGroup> builder)
        {
            builder.HasMany(x => x.GroupStudents)
                .WithOne(x => x.HomeWorkGroup)
                .HasForeignKey(x => x.GroupId);

            builder.HasMany(x => x.GroupTeachers)
                .WithOne(x => x.HomeWorkGroup)
                .HasForeignKey(x => x.GroupId);

            builder.HasMany(x => x.Lessons)
                .WithOne(x => x.HomeWorkGroup)
                .HasForeignKey(x => x.GroupId);
        }
    }
}