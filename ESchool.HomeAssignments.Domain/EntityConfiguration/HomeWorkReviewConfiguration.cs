using ESchool.HomeAssignments.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.HomeAssignments.Domain.EntityConfiguration
{
    public class HomeWorkReviewConfiguration : IEntityTypeConfiguration<HomeWorkReview>
    {
        public void Configure(EntityTypeBuilder<HomeWorkReview> builder)
        {
            builder.HasOne(x => x.HomeWorkSolution)
                .WithOne(x => x.HomeWorkReview)
                .HasForeignKey<HomeWorkReview>(x => x.HomeWorkSolutionId);

            builder.HasOne(x => x.Reviewer)
                .WithMany()
                .HasForeignKey(x => x.ReviewerId);
        }
    }
}