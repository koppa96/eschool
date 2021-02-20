using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ESchool.Testing.Domain.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SubjectName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ScheduledStart = table.Column<DateTime>(nullable: false),
                    ScheduledLength = table.Column<TimeSpan>(nullable: false),
                    StartedAt = table.Column<DateTime>(nullable: true),
                    ClosedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupStudent",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupStudent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupStudent_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupStudent_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupTeacher",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false),
                    TeacherId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTeacher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupTeacher_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupTeacher_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Started = table.Column<DateTime>(nullable: false),
                    Closed = table.Column<DateTime>(nullable: true),
                    ClosedByTeacher = table.Column<bool>(nullable: true),
                    TestId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    StudentId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answers_Students_StudentId1",
                        column: x => x.StudentId1,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Answers_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false),
                    TestId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestGroups_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GivenPoints = table.Column<int>(nullable: false),
                    HasBeenCorrected = table.Column<bool>(nullable: false),
                    TestAnswerId = table.Column<Guid>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    TestTaskId = table.Column<Guid>(nullable: true),
                    Answer = table.Column<string>(nullable: true),
                    MultipleChoiceTaskAnswer_TestTaskId = table.Column<Guid>(nullable: true),
                    SelectedOptionId = table.Column<Guid>(nullable: true),
                    TrueOrFalseTaskAnswer_TestTaskId = table.Column<Guid>(nullable: true),
                    IsTrue = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskAnswers_Answers_TestAnswerId",
                        column: x => x.TestAnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PointValue = table.Column<int>(nullable: false),
                    IncorrectAnswerPointValue = table.Column<int>(nullable: false),
                    TestId = table.Column<Guid>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    CorrectOptionId = table.Column<Guid>(nullable: true),
                    IsTrue = table.Column<bool>(nullable: true)
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
                    Id = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    TestTaskId = table.Column<Guid>(nullable: false)
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
                name: "IX_Answers_StudentId",
                table: "Answers",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_StudentId1",
                table: "Answers",
                column: "StudentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_TestId",
                table: "Answers",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupStudent_GroupId",
                table: "GroupStudent",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupStudent_StudentId",
                table: "GroupStudent",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTeacher_GroupId",
                table: "GroupTeacher",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTeacher_TeacherId",
                table: "GroupTeacher",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_MultipleChoiceTestTaskOption_TestTaskId",
                table: "MultipleChoiceTestTaskOption",
                column: "TestTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAnswers_TestTaskId",
                table: "TaskAnswers",
                column: "TestTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAnswers_SelectedOptionId",
                table: "TaskAnswers",
                column: "SelectedOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAnswers_MultipleChoiceTaskAnswer_TestTaskId",
                table: "TaskAnswers",
                column: "MultipleChoiceTaskAnswer_TestTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAnswers_TestAnswerId",
                table: "TaskAnswers",
                column: "TestAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAnswers_TrueOrFalseTaskAnswer_TestTaskId",
                table: "TaskAnswers",
                column: "TrueOrFalseTaskAnswer_TestTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CorrectOptionId",
                table: "Tasks",
                column: "CorrectOptionId",
                unique: true,
                filter: "[CorrectOptionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TestId",
                table: "Tasks",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestGroups_GroupId",
                table: "TestGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TestGroups_TestId",
                table: "TestGroups",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAnswers_Tasks_TestTaskId",
                table: "TaskAnswers",
                column: "TestTaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAnswers_Tasks_MultipleChoiceTaskAnswer_TestTaskId",
                table: "TaskAnswers",
                column: "MultipleChoiceTaskAnswer_TestTaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAnswers_Tasks_TrueOrFalseTaskAnswer_TestTaskId",
                table: "TaskAnswers",
                column: "TrueOrFalseTaskAnswer_TestTaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAnswers_MultipleChoiceTestTaskOption_SelectedOptionId",
                table: "TaskAnswers",
                column: "SelectedOptionId",
                principalTable: "MultipleChoiceTestTaskOption",
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
                name: "FK_Tasks_Tests_TestId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_MultipleChoiceTestTaskOption_Tasks_TestTaskId",
                table: "MultipleChoiceTestTaskOption");

            migrationBuilder.DropTable(
                name: "GroupStudent");

            migrationBuilder.DropTable(
                name: "GroupTeacher");

            migrationBuilder.DropTable(
                name: "TaskAnswers");

            migrationBuilder.DropTable(
                name: "TestGroups");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "MultipleChoiceTestTaskOption");
        }
    }
}
