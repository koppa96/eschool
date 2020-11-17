﻿// <auto-generated />
using System;
using ESchool.Testing.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ESchool.Testing.Domain.Migrations
{
    [DbContext(typeof(TestingContext))]
    [Migration("20201117231905_AddTeacherAndManyToManyStudents")]
    partial class AddTeacherAndManyToManyStudents
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.Answers.TaskAnswer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GivenPoints")
                        .HasColumnType("int");

                    b.Property<bool>("HasBeenCorrected")
                        .HasColumnType("bit");

                    b.Property<Guid>("TestAnswerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TestAnswerId");

                    b.ToTable("TaskAnswers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("TaskAnswer");
                });

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.ClassRegisterData.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.ClassRegisterData.GroupStudent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("StudentId");

                    b.ToTable("GroupStudent");
                });

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.ClassRegisterData.GroupTeacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("TeacherId");

                    b.ToTable("GroupTeacher");
                });

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.ClassRegisterData.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.ClassRegisterData.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.Tasks.MultipleChoice.MultipleChoiceTestTaskOption", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TestTaskId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TestTaskId");

                    b.ToTable("MultipleChoiceTestTaskOption");
                });

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.Tasks.TestTask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IncorrectAnswerPointValue")
                        .HasColumnType("int");

                    b.Property<int>("PointValue")
                        .HasColumnType("int");

                    b.Property<Guid>("TestId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("Tasks");

                    b.HasDiscriminator<string>("Discriminator").HasValue("TestTask");
                });

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.Test", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ClosedAt")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("ScheduledLength")
                        .HasColumnType("time");

                    b.Property<DateTime>("ScheduledStart")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("StartedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.TestAnswer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Closed")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("ClosedByTeacher")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Started")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StudentId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TestId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("StudentId1");

                    b.HasIndex("TestId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.TestGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TestId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("TestId");

                    b.ToTable("TestGroups");
                });

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.Answers.FreeText.FreeTextTaskAnswer", b =>
                {
                    b.HasBaseType("ESchool.Testing.Domain.Entities.Answers.TaskAnswer");

                    b.Property<string>("Answer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TestTaskId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("TestTaskId");

                    b.HasDiscriminator().HasValue("FreeTextTaskAnswer");
                });

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.Answers.MultipleChoice.MultipleChoiceTaskAnswer", b =>
                {
                    b.HasBaseType("ESchool.Testing.Domain.Entities.Answers.TaskAnswer");

                    b.Property<Guid?>("SelectedOptionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TestTaskId")
                        .HasColumnName("MultipleChoiceTaskAnswer_TestTaskId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("SelectedOptionId");

                    b.HasIndex("TestTaskId");

                    b.HasDiscriminator().HasValue("MultipleChoiceTaskAnswer");
                });

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.Answers.TrueOrFalse.TrueOrFalseTaskAnswer", b =>
                {
                    b.HasBaseType("ESchool.Testing.Domain.Entities.Answers.TaskAnswer");

                    b.Property<bool>("IsTrue")
                        .HasColumnType("bit");

                    b.Property<Guid?>("TestTaskId")
                        .HasColumnName("TrueOrFalseTaskAnswer_TestTaskId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("TestTaskId");

                    b.HasDiscriminator().HasValue("TrueOrFalseTaskAnswer");
                });

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.Tasks.FreeText.FreeTextTestTask", b =>
                {
                    b.HasBaseType("ESchool.Testing.Domain.Entities.Tasks.TestTask");

                    b.HasDiscriminator().HasValue("FreeTextTestTask");
                });

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.Tasks.MultipleChoice.MultipleChoiceTestTask", b =>
                {
                    b.HasBaseType("ESchool.Testing.Domain.Entities.Tasks.TestTask");

                    b.Property<Guid?>("CorrectOptionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("CorrectOptionId")
                        .IsUnique()
                        .HasFilter("[CorrectOptionId] IS NOT NULL");

                    b.HasDiscriminator().HasValue("MultipleChoiceTestTask");
                });

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.Tasks.TrueOrFalse.TrueOrFalseTestTask", b =>
                {
                    b.HasBaseType("ESchool.Testing.Domain.Entities.Tasks.TestTask");

                    b.Property<bool>("IsTrue")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("TrueOrFalseTestTask");
                });

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.Answers.TaskAnswer", b =>
                {
                    b.HasOne("ESchool.Testing.Domain.Entities.TestAnswer", "TestAnswer")
                        .WithMany("TaskAnswers")
                        .HasForeignKey("TestAnswerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.ClassRegisterData.GroupStudent", b =>
                {
                    b.HasOne("ESchool.Testing.Domain.Entities.ClassRegisterData.Group", "Group")
                        .WithMany("GroupStudents")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESchool.Testing.Domain.Entities.ClassRegisterData.Student", "Student")
                        .WithMany("GroupStudents")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.ClassRegisterData.GroupTeacher", b =>
                {
                    b.HasOne("ESchool.Testing.Domain.Entities.ClassRegisterData.Group", "Group")
                        .WithMany("GroupTeachers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESchool.Testing.Domain.Entities.ClassRegisterData.Teacher", "Teacher")
                        .WithMany("GroupTeachers")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.Tasks.MultipleChoice.MultipleChoiceTestTaskOption", b =>
                {
                    b.HasOne("ESchool.Testing.Domain.Entities.Tasks.MultipleChoice.MultipleChoiceTestTask", "TestTask")
                        .WithMany("Options")
                        .HasForeignKey("TestTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.Tasks.TestTask", b =>
                {
                    b.HasOne("ESchool.Testing.Domain.Entities.Test", "Test")
                        .WithMany("Tasks")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.TestAnswer", b =>
                {
                    b.HasOne("ESchool.Testing.Domain.Entities.ClassRegisterData.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESchool.Testing.Domain.Entities.ClassRegisterData.Student", null)
                        .WithMany("TestAnswers")
                        .HasForeignKey("StudentId1");

                    b.HasOne("ESchool.Testing.Domain.Entities.Test", "Test")
                        .WithMany("Answers")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.TestGroup", b =>
                {
                    b.HasOne("ESchool.Testing.Domain.Entities.ClassRegisterData.Group", "Group")
                        .WithMany("TestGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESchool.Testing.Domain.Entities.Test", "Test")
                        .WithMany("TestGroups")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.Answers.FreeText.FreeTextTaskAnswer", b =>
                {
                    b.HasOne("ESchool.Testing.Domain.Entities.Tasks.FreeText.FreeTextTestTask", "TestTask")
                        .WithMany()
                        .HasForeignKey("TestTaskId");
                });

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.Answers.MultipleChoice.MultipleChoiceTaskAnswer", b =>
                {
                    b.HasOne("ESchool.Testing.Domain.Entities.Tasks.MultipleChoice.MultipleChoiceTestTaskOption", "SelectedOption")
                        .WithMany()
                        .HasForeignKey("SelectedOptionId");

                    b.HasOne("ESchool.Testing.Domain.Entities.Tasks.MultipleChoice.MultipleChoiceTestTask", "TestTask")
                        .WithMany()
                        .HasForeignKey("TestTaskId");
                });

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.Answers.TrueOrFalse.TrueOrFalseTaskAnswer", b =>
                {
                    b.HasOne("ESchool.Testing.Domain.Entities.Tasks.TrueOrFalse.TrueOrFalseTestTask", "TestTask")
                        .WithMany()
                        .HasForeignKey("TestTaskId");
                });

            modelBuilder.Entity("ESchool.Testing.Domain.Entities.Tasks.MultipleChoice.MultipleChoiceTestTask", b =>
                {
                    b.HasOne("ESchool.Testing.Domain.Entities.Tasks.MultipleChoice.MultipleChoiceTestTaskOption", "CorrectOption")
                        .WithOne()
                        .HasForeignKey("ESchool.Testing.Domain.Entities.Tasks.MultipleChoice.MultipleChoiceTestTask", "CorrectOptionId");
                });
#pragma warning restore 612, 618
        }
    }
}
