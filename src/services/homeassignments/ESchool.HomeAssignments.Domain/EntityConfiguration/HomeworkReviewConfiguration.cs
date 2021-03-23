using ESchool.HomeAssignments.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.HomeAssignments.Domain.EntityConfiguration
{
    public class HomeworkReviewConfiguration : IEntityTypeConfiguration<HomeworkReview>
    {
        public void Configure(EntityTypeBuilder<HomeworkReview> builder)
        {
            builder.HasOne(x => x.CreatedBy)
                .WithMany()
                .HasForeignKey(x => x.CreatedById);

            builder.HasOne(x => x.LastModifiedBy)
                .WithMany()
                .HasForeignKey(x => x.LastModifiedById);
        }
    }
}