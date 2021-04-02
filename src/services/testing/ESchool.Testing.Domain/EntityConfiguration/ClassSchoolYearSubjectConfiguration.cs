using ESchool.Testing.Domain.Entities.ClassRegisterData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.Testing.Domain.EntityConfiguration
{
    public class ClassSchoolYearSubjectConfiguration : IEntityTypeConfiguration<ClassSchoolYearSubject>
    {
        public void Configure(EntityTypeBuilder<ClassSchoolYearSubject> builder)
        {
            builder.HasMany(x => x.Tests)
                .WithOne(x => x.ClassSchoolYearSubject)
                .HasForeignKey(x => x.ClassSchoolYearSubjectId);

            builder.HasMany(x => x.ClassSchoolYearSubjectStudents)
                .WithOne(x => x.ClassSchoolYearSubject)
                .HasForeignKey(x => x.ClassSchoolYearSubjectId);
        }
    }
}