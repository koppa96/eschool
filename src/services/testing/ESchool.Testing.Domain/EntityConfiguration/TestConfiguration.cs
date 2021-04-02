using ESchool.Testing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.Testing.Domain.EntityConfiguration
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.HasMany(x => x.Tasks)
                .WithOne(x => x.Test)
                .HasForeignKey(x => x.TestId);

            builder.HasMany(x => x.StudentTests)
                .WithOne(x => x.Test)
                .HasForeignKey(x => x.TestId);
        }
    }
}