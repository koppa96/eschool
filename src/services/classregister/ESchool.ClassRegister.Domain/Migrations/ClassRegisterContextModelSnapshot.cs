﻿// <auto-generated />
using System;
using ESchool.ClassRegister.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ESchool.ClassRegister.Domain.Migrations
{
    [DbContext(typeof(ClassRegisterContext))]
    partial class ClassRegisterContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Class", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClassTypeId")
                        .HasColumnType("uuid");

                    b.Property<bool>("DidFinish")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("HeadTeacherId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ClassTypeId");

                    b.HasIndex("HeadTeacherId");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.ClassSchoolYear", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClassId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SchoolYearId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SchoolYearId");

                    b.HasIndex("ClassId", "SchoolYearId")
                        .IsUnique();

                    b.ToTable("ClassSchoolYears");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.ClassType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("StartingGrade")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("ClassTypes");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Classroom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Classrooms");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Grading.Grade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClassSchoolYearSubjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ClassSchoolYearSubjectId1")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid>("KindId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("StudentId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("TeacherId")
                        .HasColumnType("uuid");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.Property<DateTime>("WrittenIn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ClassSchoolYearSubjectId");

                    b.HasIndex("ClassSchoolYearSubjectId1");

                    b.HasIndex("KindId");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Grading.GradeKind", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("AverageMultiplier")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("GradeKinds");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Messaging.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SenderUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Subject")
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SenderUserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Messaging.RecipientGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RecipientGroups");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Messaging.RecipientGroupMember", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("MemberId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RecipientGroupId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.HasIndex("RecipientGroupId");

                    b.ToTable("RecipientGroupMembers");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Messaging.UserMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsRead")
                        .HasColumnType("boolean");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("MessageId");

                    b.HasIndex("UserId");

                    b.ToTable("UserMessages");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.SchoolYear", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DisplayName")
                        .HasColumnType("text");

                    b.Property<DateTime>("EndOfFirstHalf")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("EndsAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("StartsAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("SchoolYears");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Subject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.SubjectManagement.Absence", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AbsenceState")
                        .HasColumnType("integer");

                    b.Property<Guid>("LessonId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.HasIndex("StudentId");

                    b.ToTable("Absences");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.SubjectManagement.ClassSchoolYearSubject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClassSchoolYearId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ClassSchoolYearId");

                    b.HasIndex("SubjectId");

                    b.ToTable("ClassSchoolYearSubjects");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.SubjectManagement.ClassSchoolYearSubjectTeacher", b =>
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

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.SubjectManagement.Lesson", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Canceled")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ClassSchoolYearSubjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClassroomId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("EndsAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("StartsAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ClassSchoolYearSubjectId");

                    b.HasIndex("ClassroomId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.SubjectManagement.SubjectTeacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TeacherId");

                    b.ToTable("SubjectTeachers");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Users.Abstractions.ClassRegisterUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Users.Abstractions.ClassRegisterUserRole", b =>
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

                    b.HasDiscriminator<string>("Discriminator").HasValue("ClassRegisterUserRole");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Users.StudentParent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ParentId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("StudentId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentParents");
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

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Users.Administrator", b =>
                {
                    b.HasBaseType("ESchool.ClassRegister.Domain.Entities.Users.Abstractions.ClassRegisterUserRole");

                    b.HasDiscriminator().HasValue("Administrator");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Users.Parent", b =>
                {
                    b.HasBaseType("ESchool.ClassRegister.Domain.Entities.Users.Abstractions.ClassRegisterUserRole");

                    b.HasDiscriminator().HasValue("Parent");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Users.Student", b =>
                {
                    b.HasBaseType("ESchool.ClassRegister.Domain.Entities.Users.Abstractions.ClassRegisterUserRole");

                    b.Property<Guid?>("ClassId")
                        .HasColumnType("uuid");

                    b.Property<string>("StudentIdentificationNumber")
                        .HasColumnType("text");

                    b.HasIndex("ClassId");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Users.Teacher", b =>
                {
                    b.HasBaseType("ESchool.ClassRegister.Domain.Entities.Users.Abstractions.ClassRegisterUserRole");

                    b.Property<Guid?>("CurrentClassId")
                        .HasColumnType("uuid");

                    b.HasIndex("CurrentClassId")
                        .IsUnique();

                    b.HasDiscriminator().HasValue("Teacher");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Class", b =>
                {
                    b.HasOne("ESchool.ClassRegister.Domain.Entities.ClassType", "ClassType")
                        .WithMany()
                        .HasForeignKey("ClassTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESchool.ClassRegister.Domain.Entities.Users.Teacher", "HeadTeacher")
                        .WithMany("PreviousClasses")
                        .HasForeignKey("HeadTeacherId");

                    b.Navigation("ClassType");

                    b.Navigation("HeadTeacher");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.ClassSchoolYear", b =>
                {
                    b.HasOne("ESchool.ClassRegister.Domain.Entities.Class", "Class")
                        .WithMany("ClassSchoolYears")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESchool.ClassRegister.Domain.Entities.SchoolYear", "SchoolYear")
                        .WithMany("ClassSchoolYears")
                        .HasForeignKey("SchoolYearId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("SchoolYear");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Grading.Grade", b =>
                {
                    b.HasOne("ESchool.ClassRegister.Domain.Entities.SubjectManagement.ClassSchoolYearSubject", "ClassSchoolYearSubject")
                        .WithMany()
                        .HasForeignKey("ClassSchoolYearSubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESchool.ClassRegister.Domain.Entities.SubjectManagement.ClassSchoolYearSubject", null)
                        .WithMany("Grades")
                        .HasForeignKey("ClassSchoolYearSubjectId1");

                    b.HasOne("ESchool.ClassRegister.Domain.Entities.Grading.GradeKind", "Kind")
                        .WithMany()
                        .HasForeignKey("KindId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESchool.ClassRegister.Domain.Entities.Users.Student", "Student")
                        .WithMany("Grades")
                        .HasForeignKey("StudentId");

                    b.HasOne("ESchool.ClassRegister.Domain.Entities.Users.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId");

                    b.Navigation("ClassSchoolYearSubject");

                    b.Navigation("Kind");

                    b.Navigation("Student");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Messaging.Message", b =>
                {
                    b.HasOne("ESchool.ClassRegister.Domain.Entities.Users.Abstractions.ClassRegisterUser", "SenderClassRegisterUser")
                        .WithMany("SentMessages")
                        .HasForeignKey("SenderUserId");

                    b.Navigation("SenderClassRegisterUser");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Messaging.RecipientGroup", b =>
                {
                    b.HasOne("ESchool.ClassRegister.Domain.Entities.Users.Abstractions.ClassRegisterUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Messaging.RecipientGroupMember", b =>
                {
                    b.HasOne("ESchool.ClassRegister.Domain.Entities.Users.Abstractions.ClassRegisterUser", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESchool.ClassRegister.Domain.Entities.Messaging.RecipientGroup", "RecipientGroup")
                        .WithMany("Members")
                        .HasForeignKey("RecipientGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("RecipientGroup");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Messaging.UserMessage", b =>
                {
                    b.HasOne("ESchool.ClassRegister.Domain.Entities.Messaging.Message", "Message")
                        .WithMany("ReceiverUserMessages")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESchool.ClassRegister.Domain.Entities.Users.Abstractions.ClassRegisterUser", "ClassRegisterUser")
                        .WithMany("ReceivedMessages")
                        .HasForeignKey("UserId");

                    b.Navigation("ClassRegisterUser");

                    b.Navigation("Message");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.SubjectManagement.Absence", b =>
                {
                    b.HasOne("ESchool.ClassRegister.Domain.Entities.SubjectManagement.Lesson", "Lesson")
                        .WithMany("Absences")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESchool.ClassRegister.Domain.Entities.Users.Student", "Student")
                        .WithMany("Absences")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.SubjectManagement.ClassSchoolYearSubject", b =>
                {
                    b.HasOne("ESchool.ClassRegister.Domain.Entities.ClassSchoolYear", "ClassSchoolYear")
                        .WithMany("ClassSchoolYearSubjects")
                        .HasForeignKey("ClassSchoolYearId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESchool.ClassRegister.Domain.Entities.Subject", "Subject")
                        .WithMany("ClassSchoolYearSubjects")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClassSchoolYear");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.SubjectManagement.ClassSchoolYearSubjectTeacher", b =>
                {
                    b.HasOne("ESchool.ClassRegister.Domain.Entities.SubjectManagement.ClassSchoolYearSubject", "ClassSchoolYearSubject")
                        .WithMany("ClassSchoolYearSubjectTeachers")
                        .HasForeignKey("ClassSchoolYearSubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESchool.ClassRegister.Domain.Entities.Users.Teacher", "Teacher")
                        .WithMany("ClassSchoolYearSubjectTeachers")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClassSchoolYearSubject");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.SubjectManagement.Lesson", b =>
                {
                    b.HasOne("ESchool.ClassRegister.Domain.Entities.SubjectManagement.ClassSchoolYearSubject", "ClassSchoolYearSubject")
                        .WithMany("Lessons")
                        .HasForeignKey("ClassSchoolYearSubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESchool.ClassRegister.Domain.Entities.Classroom", "Classroom")
                        .WithMany("Lessons")
                        .HasForeignKey("ClassroomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Classroom");

                    b.Navigation("ClassSchoolYearSubject");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.SubjectManagement.SubjectTeacher", b =>
                {
                    b.HasOne("ESchool.ClassRegister.Domain.Entities.Subject", "Subject")
                        .WithMany("SubjectTeachers")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESchool.ClassRegister.Domain.Entities.Users.Teacher", "Teacher")
                        .WithMany("SubjectTeachers")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Users.Abstractions.ClassRegisterUserRole", b =>
                {
                    b.HasOne("ESchool.ClassRegister.Domain.Entities.Users.Abstractions.ClassRegisterUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Users.StudentParent", b =>
                {
                    b.HasOne("ESchool.ClassRegister.Domain.Entities.Users.Parent", "Parent")
                        .WithMany("StudentParents")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESchool.ClassRegister.Domain.Entities.Users.Student", "Student")
                        .WithMany("StudentParents")
                        .HasForeignKey("StudentId");

                    b.Navigation("Parent");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Users.Student", b =>
                {
                    b.HasOne("ESchool.ClassRegister.Domain.Entities.Class", "Class")
                        .WithMany("Students")
                        .HasForeignKey("ClassId");

                    b.Navigation("Class");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Users.Teacher", b =>
                {
                    b.HasOne("ESchool.ClassRegister.Domain.Entities.Class", "CurrentClass")
                        .WithOne()
                        .HasForeignKey("ESchool.ClassRegister.Domain.Entities.Users.Teacher", "CurrentClassId");

                    b.Navigation("CurrentClass");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Class", b =>
                {
                    b.Navigation("ClassSchoolYears");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.ClassSchoolYear", b =>
                {
                    b.Navigation("ClassSchoolYearSubjects");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Classroom", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Messaging.Message", b =>
                {
                    b.Navigation("ReceiverUserMessages");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Messaging.RecipientGroup", b =>
                {
                    b.Navigation("Members");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.SchoolYear", b =>
                {
                    b.Navigation("ClassSchoolYears");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Subject", b =>
                {
                    b.Navigation("ClassSchoolYearSubjects");

                    b.Navigation("SubjectTeachers");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.SubjectManagement.ClassSchoolYearSubject", b =>
                {
                    b.Navigation("ClassSchoolYearSubjectTeachers");

                    b.Navigation("Grades");

                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.SubjectManagement.Lesson", b =>
                {
                    b.Navigation("Absences");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Users.Abstractions.ClassRegisterUser", b =>
                {
                    b.Navigation("ReceivedMessages");

                    b.Navigation("SentMessages");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Users.Parent", b =>
                {
                    b.Navigation("StudentParents");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Users.Student", b =>
                {
                    b.Navigation("Absences");

                    b.Navigation("Grades");

                    b.Navigation("StudentParents");
                });

            modelBuilder.Entity("ESchool.ClassRegister.Domain.Entities.Users.Teacher", b =>
                {
                    b.Navigation("ClassSchoolYearSubjectTeachers");

                    b.Navigation("PreviousClasses");

                    b.Navigation("SubjectTeachers");
                });
#pragma warning restore 612, 618
        }
    }
}
