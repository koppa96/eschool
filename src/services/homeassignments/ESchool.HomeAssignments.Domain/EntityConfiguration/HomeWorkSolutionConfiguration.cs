using ESchool.HomeAssignments.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.HomeAssignments.Domain.EntityConfiguration
{
    public class HomeWorkSolutionConfiguration : IEntityTypeConfiguration<HomeWorkSolution>
    {
        public void Configure(EntityTypeBuilder<HomeWorkSolution> builder)
        {
            builder.HasMany(x => x.Files)
                .WithOne(x => x.HomeWorkSolution)
                .HasForeignKey(x => x.HomeWorkSolutionId);
        }
    }
}