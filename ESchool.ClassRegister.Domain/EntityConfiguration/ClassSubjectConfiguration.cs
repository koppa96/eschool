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

            builder.HasOne(x => x.Class)
                .WithMany(x => x.ClassSubjects)
                .HasForeignKey(x => x.ClassId);

            builder.HasOne(x => x.SchoolYear)
                .WithMany()
                .HasForeignKey(x => x.SchoolYearId);

            builder.HasOne(x => x.Subject)
                .WithMany()
                .HasForeignKey(x => x.SubjectId);
        }
    }
}