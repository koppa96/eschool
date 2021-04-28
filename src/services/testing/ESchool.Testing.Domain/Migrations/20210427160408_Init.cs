using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ESchool.Testing.Domain.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassSchoolYearSubjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClassId = table.Column<Guid>(type: "uuid", nullable: false),
                    SchoolYearId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSchoolYearSubjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutboxEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Headers = table.Column<string>(type: "text", nullable: true),
                    Body = table.Column<string>(type: "text", nullable: true),
                    TypeName = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    Retries = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ScheduledStart = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ScheduledLength = table.Column<TimeSpan>(type: "interval", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ClosedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ClassSchoolYearSubjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tests_ClassSchoolYearSubjects_ClassSchoolYearSubjectId",
                        column: x => x.ClassSchoolYearSubjectId,
                        principalTable: "ClassSchoolYearSubjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassSchoolYearSubjectStudents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClassSchoolYearSubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSchoolYearSubjectStudents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassSchoolYearSubjectStudents_ClassSchoolYearSubjects_Clas~",
                        column: x => x.ClassSchoolYearSubjectId,
                        principalTable: "ClassSchoolYearSubjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassSchoolYearSubjectStudents_UserRoles_StudentId",
                        column: x => x.StudentId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassSchoolYearSubjectTeachers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClassSchoolYearSubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSchoolYearSubjectTeachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassSchoolYearSubjectTeachers_ClassSchoolYearSubjects_Clas~",
                        column: x => x.ClassSchoolYearSubjectId,
                        principalTable: "ClassSchoolYearSubjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassSchoolYearSubjectTeachers_UserRoles_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Started = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Closed = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ClosedByTeacher = table.Column<bool>(type: "boolean", nullable: true),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: true),
                    TestId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestAnswers_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestAnswers_UserRoles_StudentId",
                        column: x => x.StudentId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentTest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    TestId = table.Column<Guid>(type: "uuid", nullable: false),
                    TestAnswerId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentTest_TestAnswers_TestAnswerId",
                        column: x => x.TestAnswerId,
                        principalTable: "TestAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentTest_TestAnswers_TestId",
                        column: x => x.TestId,
                        principalTable: "TestAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentTest_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentTest_UserRoles_StudentId",
                        column: x => x.StudentId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GivenPoints = table.Column<int>(type: "integer", nullable: false),
                    HasBeenCorrected = table.Column<bool>(type: "boolean", nullable: false),
                    TestAnswerId = table.Column<Guid>(type: "uuid", nullable: false),
                    TestTaskId = table.Column<Guid>(type: "uuid", nullable: true),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    Answer = table.Column<string>(type: "text", nullable: true),
                    SelectedOptionId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsTrue = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskAnswers_TestAnswers_TestAnswerId",
                        column: x => x.TestAnswerId,
                        principalTable: "TestAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PointValue = table.Column<int>(type: "integer", nullable: false),
                    IncorrectAnswerPointValue = table.Column<int>(type: "integer", nullable: false),
                    TestId = table.Column<Guid>(type: "uuid", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    CorrectOptionId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsTrue = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MultipleChoiceTestTaskOption",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true),
                    TestTaskId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultipleChoiceTestTaskOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultipleChoiceTestTaskOption_Tasks_TestTaskId",
                        column: x => x.TestTaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchoolYearSubjectStudents_ClassSchoolYearSubjectId",
                table: "ClassSchoolYearSubjectStudents",
                column: "ClassSchoolYearSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchoolYearSubjectStudents_StudentId",
                table: "ClassSchoolYearSubjectStudents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchoolYearSubjectTeachers_ClassSchoolYearSubjectId",
                table: "ClassSchoolYearSubjectTeachers",
                column: "ClassSchoolYearSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchoolYearSubjectTeachers_TeacherId",
                table: "ClassSchoolYearSubjectTeachers",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_MultipleChoiceTestTaskOption_TestTaskId",
                table: "MultipleChoiceTestTaskOption",
                column: "TestTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxEntries_State",
                table: "OutboxEntries",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTest_StudentId",
                table: "StudentTest",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTest_TestAnswerId",
                table: "StudentTest",
                column: "TestAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTest_TestId",
                table: "StudentTest",
                column: "TestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskAnswers_SelectedOptionId",
                table: "TaskAnswers",
                column: "SelectedOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAnswers_TestAnswerId",
                table: "TaskAnswers",
                column: "TestAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAnswers_TestTaskId",
                table: "TaskAnswers",
                column: "TestTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CorrectOptionId",
                table: "Tasks",
                column: "CorrectOptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TestId",
                table: "Tasks",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestAnswers_StudentId",
                table: "TestAnswers",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TestAnswers_TestId",
                table: "TestAnswers",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_ClassSchoolYearSubjectId",
                table: "Tests",
                column: "ClassSchoolYearSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAnswers_MultipleChoiceTestTaskOption_SelectedOptionId",
                table: "TaskAnswers",
                column: "SelectedOptionId",
                principalTable: "MultipleChoiceTestTaskOption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAnswers_Tasks_TestTaskId",
                table: "TaskAnswers",
                column: "TestTaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_MultipleChoiceTestTaskOption_CorrectOptionId",
                table: "Tasks",
                column: "CorrectOptionId",
                principalTable: "MultipleChoiceTestTaskOption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_ClassSchoolYearSubjects_ClassSchoolYearSubjectId",
                table: "Tests");

            migrationBuilder.DropForeignKey(
                name: "FK_MultipleChoiceTestTaskOption_Tasks_TestTaskId",
                table: "MultipleChoiceTestTaskOption");

            migrationBuilder.DropTable(
                name: "ClassSchoolYearSubjectStudents");

            migrationBuilder.DropTable(
                name: "ClassSchoolYearSubjectTeachers");

            migrationBuilder.DropTable(
                name: "OutboxEntries");

            migrationBuilder.DropTable(
                name: "StudentTest");

            migrationBuilder.DropTable(
                name: "TaskAnswers");

            migrationBuilder.DropTable(
                name: "TestAnswers");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ClassSchoolYearSubjects");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "MultipleChoiceTestTaskOption");

            migrationBuilder.DropTable(
                name: "Tests");
        }
    }
}
