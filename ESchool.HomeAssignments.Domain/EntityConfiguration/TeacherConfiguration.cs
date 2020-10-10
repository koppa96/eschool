using ESchool.HomeAssignments.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.HomeAssignments.Domain.EntityConfiguration
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasMany(x => x.GroupTeachers)
                .WithOne(x => x.Teacher)
                .HasForeignKey(x => x.TeacherId);
        }
    }
}