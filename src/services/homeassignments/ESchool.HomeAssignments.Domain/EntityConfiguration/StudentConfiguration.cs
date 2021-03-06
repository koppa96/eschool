﻿using ESchool.HomeAssignments.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.HomeAssignments.Domain.EntityConfiguration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasBaseType<HomeAssignmentsUserRole>();
            
            builder.HasMany(x => x.Solutions)
                .WithOne(x => x.Student)
                .HasForeignKey(x => x.StudentId);

            builder.HasMany(x => x.ClassSchoolYearSubjectStudents)
                .WithOne(x => x.Student)
                .HasForeignKey(x => x.StudentId);
        }
    }
}