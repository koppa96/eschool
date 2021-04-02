using ESchool.Testing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.Testing.Domain.EntityConfiguration
{
    public class TestAnswerConfiguration : IEntityTypeConfiguration<TestAnswer>
    {
        public void Configure(EntityTypeBuilder<TestAnswer> builder)
        {
            builder.HasMany(x => x.TaskAnswers)
                .WithOne(x => x.TestAnswer)
                .HasForeignKey(x => x.TestAnswerId);

            builder.HasOne(x => x.StudentTest)
                .WithOne()
                .HasForeignKey<StudentTest>(x => x.TestId);
        }
    }
}