using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.ClassRegister.Domain.EntityConfiguration
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasMany(x => x.Absences)
                .WithOne(x => x.Lesson)
                .HasForeignKey(x => x.LessonId);

            builder.HasMany(x => x.HomeWorks)
                .WithOne(x => x.Lesson)
                .HasForeignKey(x => x.LessonId);

            builder.HasOne(x => x.ClassSchoolYearSubject)
                .WithMany(x => x.Lessons)
                .HasForeignKey(x => x.ClassSchoolYearSubjectId);

            builder.HasOne(x => x.ClassRoom)
                .WithMany(x => x.Lessons)
                .HasForeignKey(x => x.ClassRoomId);
        }
    }
}