using ESchool.ClassRegister.Domain.Entities.Grading;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.ClassRegister.Domain.EntityConfiguration
{
    public class SmallGradeConfiguration : IEntityTypeConfiguration<SmallGrade>
    {
        public void Configure(EntityTypeBuilder<SmallGrade> builder)
        {
            builder.HasOne(x => x.Teacher)
                .WithMany()
                .HasForeignKey(x => x.TeacherId);

            builder.HasOne(x => x.ClassSubject)
                .WithMany()
                .HasForeignKey(x => x.ClassSubjectId);
        }
    }
}