using ESchool.Testing.Domain.Entities;
using ESchool.Testing.Domain.Entities.Answers;
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

            builder.HasOne(x => x.Student)
                .WithMany()
                .HasForeignKey(x => x.StudentId);
        }
    }
}