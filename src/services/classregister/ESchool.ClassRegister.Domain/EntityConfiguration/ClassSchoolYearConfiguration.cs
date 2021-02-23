using ESchool.ClassRegister.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.ClassRegister.Domain.EntityConfiguration
{
    public class ClassSchoolYearConfiguration : IEntityTypeConfiguration<ClassSchoolYear>
    {
        public void Configure(EntityTypeBuilder<ClassSchoolYear> builder)
        {
            builder.HasIndex(x => new { x.ClassId, x.SchoolYearId })
                .IsUnique();
        }
    }
}