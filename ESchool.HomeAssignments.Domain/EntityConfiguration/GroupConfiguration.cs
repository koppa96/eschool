using ESchool.HomeAssignments.Domain.Entities.ClassRegisterData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.HomeAssignments.Domain.EntityConfiguration
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasMany(x => x.GroupStudents)
                .WithOne(x => x.Group)
                .HasForeignKey(x => x.GroupId);

            builder.HasMany(x => x.GroupTeachers)
                .WithOne(x => x.Group)
                .HasForeignKey(x => x.GroupId);

            builder.HasMany(x => x.Lessons)
                .WithOne(x => x.Group)
                .HasForeignKey(x => x.GroupId);
        }
    }
}