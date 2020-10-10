using ESchool.ClassRegister.Domain.Entities.Grading;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.ClassRegister.Domain.EntityConfiguration
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasMany(x => x.StudentGroups)
                .WithOne(x => x.Group)
                .HasForeignKey(x => x.GroupId);

            builder.HasMany(x => x.GroupTeachers)
                .WithOne(x => x.Group)
                .HasForeignKey(x => x.TeacherId);

            builder.HasMany(x => x.ClassSubjectGroups)
                .WithOne(x => x.Group)
                .HasForeignKey(x => x.GroupId);

            builder.HasOne(x => x.SmallGradesPolicy)
                .WithOne(x => x.Group)
                .HasForeignKey<SmallGradesPolicy>(x => x.GroupId);
        }
    }
}