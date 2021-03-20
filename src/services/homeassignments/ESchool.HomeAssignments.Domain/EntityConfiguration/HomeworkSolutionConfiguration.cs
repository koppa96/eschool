using ESchool.HomeAssignments.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.HomeAssignments.Domain.EntityConfiguration
{
    public class HomeworkSolutionConfiguration : IEntityTypeConfiguration<HomeworkSolution>
    {
        public void Configure(EntityTypeBuilder<HomeworkSolution> builder)
        {
            builder.HasMany(x => x.Files)
                .WithOne(x => x.HomeworkSolution)
                .HasForeignKey(x => x.HomeWorkSolutionId);

            builder.HasOne(x => x.HomeworkReview)
                .WithOne(x => x.HomeworkSolution)
                .HasForeignKey<HomeworkReview>(x => x.HomeWorkSolutionId);
        }
    }
}