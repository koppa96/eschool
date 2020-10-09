using ESchool.Testing.Domain.Entities;
using ESchool.Testing.Domain.Entities.ClassRegisterData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.Testing.Domain.EntityConfiguration
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasMany(x => x.TestGroups)
                .WithOne(x => x.Group)
                .HasForeignKey(x => x.GroupId);

            builder.HasMany(x => x.Students)
                .WithOne(x => x.Group)
                .HasForeignKey(x => x.GroupId);
        }
    }
}