﻿// <auto-generated />
using System;
using ESchool.HomeAssignments.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ESchool.HomeAssignments.Domain.Migrations
{
    [DbContext(typeof(HomeAssignmentsContext))]
    partial class HomeAssignmentsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.ClassRegisterData.ClassSchoolYearSubject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("ClassSchoolYearSubjects");
                });

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.ClassRegisterData.ClassSchoolYearSubjectStudent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClassSchoolYearSubjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ClassSchoolYearSubjectId");

                    b.HasIndex("StudentId");

                    b.ToTable("ClassSchoolYearSubjectStudents");
                });

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.ClassRegisterData.ClassSchoolYearSubjectTeacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClassSchoolYearSubjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ClassSchoolYearSubjectId");

                    b.HasIndex("TeacherId");

                    b.ToTable("ClassSchoolYearSubjectTeachers");
                });

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.ClassRegisterData.Lesson", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClassSchoolYearSubjectId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ClassSchoolYearSubjectId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FileName")
                        .HasColumnType("text");

                    b.Property<Guid>("HomeWorkSolutionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("HomeWorkSolutionId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.Homework", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("LastModifiedById")
                        .HasColumnType("uuid");

                    b.Property<Guid>("LessonId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Optional")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("LastModifiedById");

                    b.HasIndex("LessonId");

                    b.ToTable("Homeworks");
                });

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.HomeworkReview", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<Guid>("HomeWorkSolutionId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("LastModifiedById")
                        .HasColumnType("uuid");

                    b.Property<int>("Outcome")
                        .HasColumnType("integer");

                    b.Property<Guid?>("TeacherId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("HomeWorkSolutionId")
                        .IsUnique();

                    b.HasIndex("LastModifiedById");

                    b.HasIndex("TeacherId");

                    b.ToTable("HomeworkReviews");
                });

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.HomeworkSolution", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("HomeworkId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("StudentId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("TurnInDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("HomeworkId");

                    b.HasIndex("StudentId");

                    b.ToTable("HomeworkSolutions");
                });

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.Users.HomeAssignmentsUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.Users.HomeAssignmentsUserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("HomeAssignmentsUserRole");
                });

            modelBuilder.Entity("ESchool.Libs.Outbox.EntityFrameworkCore.Entities.OutboxEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Body")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Headers")
                        .HasColumnType("text");

                    b.Property<int>("Retries")
                        .HasColumnType("integer");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<string>("TypeName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("State");

                    b.ToTable("OutboxEntries");
                });

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.Users.Student", b =>
                {
                    b.HasBaseType("ESchool.HomeAssignments.Domain.Entities.Users.HomeAssignmentsUserRole");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.Users.Teacher", b =>
                {
                    b.HasBaseType("ESchool.HomeAssignments.Domain.Entities.Users.HomeAssignmentsUserRole");

                    b.HasDiscriminator().HasValue("Teacher");
                });

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.ClassRegisterData.ClassSchoolYearSubject", b =>
                {
                    b.OwnsOne("ESchool.HomeAssignments.Domain.Entities.ClassRegisterData.ClassRegisterEntity", "Class", b1 =>
                        {
                            b1.Property<Guid>("ClassSchoolYearSubjectId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<string>("Name")
                                .HasColumnType("text");

                            b1.HasKey("ClassSchoolYearSubjectId");

                            b1.ToTable("ClassSchoolYearSubjects");

                            b1.WithOwner()
                                .HasForeignKey("ClassSchoolYearSubjectId");
                        });

                    b.OwnsOne("ESchool.HomeAssignments.Domain.Entities.ClassRegisterData.ClassRegisterEntity", "SchoolYear", b1 =>
                        {
                            b1.Property<Guid>("ClassSchoolYearSubjectId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<string>("Name")
                                .HasColumnType("text");

                            b1.HasKey("ClassSchoolYearSubjectId");

                            b1.ToTable("ClassSchoolYearSubjects");

                            b1.WithOwner()
                                .HasForeignKey("ClassSchoolYearSubjectId");
                        });

                    b.OwnsOne("ESchool.HomeAssignments.Domain.Entities.ClassRegisterData.ClassRegisterEntity", "Subject", b1 =>
                        {
                            b1.Property<Guid>("ClassSchoolYearSubjectId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<string>("Name")
                                .HasColumnType("text");

                            b1.HasKey("ClassSchoolYearSubjectId");

                            b1.ToTable("ClassSchoolYearSubjects");

                            b1.WithOwner()
                                .HasForeignKey("ClassSchoolYearSubjectId");
                        });

                    b.Navigation("Class");

                    b.Navigation("SchoolYear");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.ClassRegisterData.ClassSchoolYearSubjectStudent", b =>
                {
                    b.HasOne("ESchool.HomeAssignments.Domain.Entities.ClassRegisterData.ClassSchoolYearSubject", "ClassSchoolYearSubject")
                        .WithMany("ClassSchoolYearSubjectStudents")
                        .HasForeignKey("ClassSchoolYearSubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESchool.HomeAssignments.Domain.Entities.Users.Student", "Student")
                        .WithMany("ClassSchoolYearSubjectStudents")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClassSchoolYearSubject");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.ClassRegisterData.ClassSchoolYearSubjectTeacher", b =>
                {
                    b.HasOne("ESchool.HomeAssignments.Domain.Entities.ClassRegisterData.ClassSchoolYearSubject", "ClassSchoolYearSubject")
                        .WithMany("ClassSchoolYearSubjectTeachers")
                        .HasForeignKey("ClassSchoolYearSubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESchool.HomeAssignments.Domain.Entities.Users.Teacher", "Teacher")
                        .WithMany("ClassSchoolYearSubjectTeachers")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClassSchoolYearSubject");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.ClassRegisterData.Lesson", b =>
                {
                    b.HasOne("ESchool.HomeAssignments.Domain.Entities.ClassRegisterData.ClassSchoolYearSubject", "ClassSchoolYearSubject")
                        .WithMany("Lessons")
                        .HasForeignKey("ClassSchoolYearSubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClassSchoolYearSubject");
                });

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.File", b =>
                {
                    b.HasOne("ESchool.HomeAssignments.Domain.Entities.HomeworkSolution", "HomeworkSolution")
                        .WithMany("Files")
                        .HasForeignKey("HomeWorkSolutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HomeworkSolution");
                });

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.Homework", b =>
                {
                    b.HasOne("ESchool.HomeAssignments.Domain.Entities.Users.HomeAssignmentsUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("ESchool.HomeAssignments.Domain.Entities.Users.HomeAssignmentsUser", "LastModifiedBy")
                        .WithMany()
                        .HasForeignKey("LastModifiedById");

                    b.HasOne("ESchool.HomeAssignments.Domain.Entities.ClassRegisterData.Lesson", "Lesson")
                        .WithMany("HomeWorks")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("LastModifiedBy");

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.HomeworkReview", b =>
                {
                    b.HasOne("ESchool.HomeAssignments.Domain.Entities.Users.HomeAssignmentsUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("ESchool.HomeAssignments.Domain.Entities.HomeworkSolution", "HomeworkSolution")
                        .WithOne("HomeworkReview")
                        .HasForeignKey("ESchool.HomeAssignments.Domain.Entities.HomeworkReview", "HomeWorkSolutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESchool.HomeAssignments.Domain.Entities.Users.HomeAssignmentsUser", "LastModifiedBy")
                        .WithMany()
                        .HasForeignKey("LastModifiedById");

                    b.HasOne("ESchool.HomeAssignments.Domain.Entities.Users.Teacher", null)
                        .WithMany("Reviews")
                        .HasForeignKey("TeacherId");

                    b.Navigation("CreatedBy");

                    b.Navigation("HomeworkSolution");

                    b.Navigation("LastModifiedBy");
                });

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.HomeworkSolution", b =>
                {
                    b.HasOne("ESchool.HomeAssignments.Domain.Entities.Homework", "Homework")
                        .WithMany("Solutions")
                        .HasForeignKey("HomeworkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESchool.HomeAssignments.Domain.Entities.Users.Student", "Student")
                        .WithMany("Solutions")
                        .HasForeignKey("StudentId");

                    b.Navigation("Homework");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.Users.HomeAssignmentsUserRole", b =>
                {
                    b.HasOne("ESchool.HomeAssignments.Domain.Entities.Users.HomeAssignmentsUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.ClassRegisterData.ClassSchoolYearSubject", b =>
                {
                    b.Navigation("ClassSchoolYearSubjectStudents");

                    b.Navigation("ClassSchoolYearSubjectTeachers");

                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.ClassRegisterData.Lesson", b =>
                {
                    b.Navigation("HomeWorks");
                });

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.Homework", b =>
                {
                    b.Navigation("Solutions");
                });

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.HomeworkSolution", b =>
                {
                    b.Navigation("Files");

                    b.Navigation("HomeworkReview");
                });

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.Users.HomeAssignmentsUser", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.Users.Student", b =>
                {
                    b.Navigation("ClassSchoolYearSubjectStudents");

                    b.Navigation("Solutions");
                });

            modelBuilder.Entity("ESchool.HomeAssignments.Domain.Entities.Users.Teacher", b =>
                {
                    b.Navigation("ClassSchoolYearSubjectTeachers");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
