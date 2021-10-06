using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.ClassRegister.Domain.EntityConfiguration
{
    public class ClassSchoolYearSubjectConfiguration : IEntityTypeConfiguration<ClassSchoolYearSubject>
    {
        public void Configure(EntityTypeBuilder<ClassSchoolYearSubject> builder)
        {
            builder.HasOne(x => x.ClassSchoolYear)
                .WithMany(x => x.ClassSchoolYearSubjects)
                .HasForeignKey(x => x.ClassSchoolYearId);

            builder.HasOne(x => x.Subject)
                .WithMany(x => x.ClassSchoolYearSubjects)
                .HasForeignKey(x => x.SubjectId);
        }
    }
}