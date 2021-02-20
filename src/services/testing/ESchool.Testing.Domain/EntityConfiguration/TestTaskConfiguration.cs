using ESchool.Testing.Domain.Entities.Tasks;
using ESchool.Testing.Domain.Entities.Tasks.FreeText;
using ESchool.Testing.Domain.Entities.Tasks.MultipleChoice;
using ESchool.Testing.Domain.Entities.Tasks.TrueOrFalse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.Testing.Domain.EntityConfiguration
{
    public class TestTaskConfiguration : IEntityTypeConfiguration<FreeTextTestTask>,
        IEntityTypeConfiguration<MultipleChoiceTestTask>,
        IEntityTypeConfiguration<TrueOrFalseTestTask>
    {
        public void Configure(EntityTypeBuilder<FreeTextTestTask> builder)
        {
            builder.HasBaseType<TestTask>();
        }

        public void Configure(EntityTypeBuilder<MultipleChoiceTestTask> builder)
        {
            builder.HasBaseType<TestTask>();

            builder.HasMany(x => x.Options)
                .WithOne(x => x.TestTask)
                .HasForeignKey(x => x.TestTaskId)
                ;

            builder.HasOne(x => x.CorrectOption)
                .WithOne()
                .HasForeignKey<MultipleChoiceTestTask>(x => x.CorrectOptionId);
        }

        public void Configure(EntityTypeBuilder<TrueOrFalseTestTask> builder)
        {
            builder.HasBaseType<TestTask>();
        }
    }
}