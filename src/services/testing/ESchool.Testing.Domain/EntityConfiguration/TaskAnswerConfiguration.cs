using ESchool.Testing.Domain.Entities.Answers;
using ESchool.Testing.Domain.Entities.Answers.FreeText;
using ESchool.Testing.Domain.Entities.Answers.MultipleChoice;
using ESchool.Testing.Domain.Entities.Answers.TrueOrFalse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.Testing.Domain.EntityConfiguration
{
    public class TaskAnswerConfiguration : IEntityTypeConfiguration<FreeTextTaskAnswer>,
        IEntityTypeConfiguration<MultipleChoiceTaskAnswer>,
        IEntityTypeConfiguration<TrueOrFalseTaskAnswer>
    {
        public void Configure(EntityTypeBuilder<FreeTextTaskAnswer> builder)
        {
            builder.HasBaseType<TaskAnswer>();

            builder.HasOne(x => x.TestTask)
                .WithMany()
                .HasForeignKey(x => x.TestTaskId);
        }

        public void Configure(EntityTypeBuilder<MultipleChoiceTaskAnswer> builder)
        {
            builder.HasBaseType<TaskAnswer>();

            builder.HasOne(x => x.TestTask)
                .WithMany()
                .HasForeignKey(x => x.TestTaskId);

            builder.HasOne(x => x.SelectedOption)
                .WithMany()
                .HasForeignKey(x => x.SelectedOptionId);
        }

        public void Configure(EntityTypeBuilder<TrueOrFalseTaskAnswer> builder)
        {
            builder.HasBaseType<TaskAnswer>();

            builder.HasOne(x => x.TestTask)
                .WithMany()
                .HasForeignKey(x => x.TestTaskId);
        }
    }
}