using ESchool.ClassRegister.Domain.Entities.Grading;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.ClassRegister.Domain.EntityConfiguration
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.HasOne(x => x.Teacher)
                .WithMany()
                .HasForeignKey(x => x.TeacherId);

            builder.HasOne(x => x.Kind)
                .WithMany()
                .HasForeignKey(x => x.KindId);

            builder.HasOne(x => x.ClassSchoolYearSubject)
                .WithMany()
                .HasForeignKey(x => x.ClassSchoolYearSubjectId);
        }
    }
}