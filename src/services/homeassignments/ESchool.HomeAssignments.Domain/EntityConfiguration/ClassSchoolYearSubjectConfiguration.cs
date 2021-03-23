using ESchool.HomeAssignments.Domain.Entities.ClassRegisterData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.HomeAssignments.Domain.EntityConfiguration
{
    public class ClassSchoolYearSubjectConfiguration : IEntityTypeConfiguration<ClassSchoolYearSubject>
    {
        public void Configure(EntityTypeBuilder<ClassSchoolYearSubject> builder)
        {
            builder.HasMany(x => x.ClassSchoolYearSubjectStudents)
                .WithOne(x => x.ClassSchoolYearSubject)
                .HasForeignKey(x => x.ClassSchoolYearSubjectId);

            builder.HasMany(x => x.ClassSchoolYearSubjectTeachers)
                .WithOne(x => x.ClassSchoolYearSubject)
                .HasForeignKey(x => x.ClassSchoolYearSubjectId);

            builder.HasMany(x => x.Lessons)
                .WithOne(x => x.ClassSchoolYearSubject)
                .HasForeignKey(x => x.ClassSchoolYearSubjectId);
        }
    }
}