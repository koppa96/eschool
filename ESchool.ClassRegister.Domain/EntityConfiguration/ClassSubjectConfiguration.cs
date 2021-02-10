using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.ClassRegister.Domain.EntityConfiguration
{
    public class ClassSubjectConfiguration : IEntityTypeConfiguration<ClassSubject>
    {
        public void Configure(EntityTypeBuilder<ClassSubject> builder)
        {
            builder.HasMany(x => x.ClassSubjectGroups)
                .WithOne(x => x.ClassSubject)
                .HasForeignKey(x => x.ClassSubjectId);

            builder.HasOne(x => x.ClassSchoolYear)
                .WithMany(x => x.ClassSubjects)
                .HasForeignKey(x => x.ClassSchoolYearId);

            builder.HasOne(x => x.Subject)
                .WithMany()
                .HasForeignKey(x => x.SubjectId);
        }
    }
}