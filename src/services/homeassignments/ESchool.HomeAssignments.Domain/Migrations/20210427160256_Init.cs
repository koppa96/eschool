using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ESchool.HomeAssignments.Domain.Migrations
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
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    ClassSchoolYearSubjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lessons_ClassSchoolYearSubjects_ClassSchoolYearSubjectId",
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
                name: "Homeworks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Deadline = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Optional = table.Column<bool>(type: "boolean", nullable: false),
                    LessonId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedById = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homeworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Homeworks_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Homeworks_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Homeworks_Users_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "HomeworkSolutions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HomeworkId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: true),
                    TurnInDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeworkSolutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeworkSolutions_Homeworks_HomeworkId",
                        column: x => x.HomeworkId,
                        principalTable: "Homeworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeworkSolutions_UserRoles_StudentId",
                        column: x => x.StudentId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    HomeWorkSolutionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_HomeworkSolutions_HomeWorkSolutionId",
                        column: x => x.HomeWorkSolutionId,
                        principalTable: "HomeworkSolutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HomeworkReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    Outcome = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedById = table.Column<Guid>(type: "uuid", nullable: true),
                    HomeWorkSolutionId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeworkReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeworkReviews_HomeworkSolutions_HomeWorkSolutionId",
                        column: x => x.HomeWorkSolutionId,
                        principalTable: "HomeworkSolutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeworkReviews_UserRoles_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeworkReviews_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeworkReviews_Users_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_Files_HomeWorkSolutionId",
                table: "Files",
                column: "HomeWorkSolutionId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkReviews_CreatedById",
                table: "HomeworkReviews",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkReviews_HomeWorkSolutionId",
                table: "HomeworkReviews",
                column: "HomeWorkSolutionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkReviews_LastModifiedById",
                table: "HomeworkReviews",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkReviews_TeacherId",
                table: "HomeworkReviews",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_CreatedById",
                table: "Homeworks",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_LastModifiedById",
                table: "Homeworks",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_LessonId",
                table: "Homeworks",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkSolutions_HomeworkId",
                table: "HomeworkSolutions",
                column: "HomeworkId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkSolutions_StudentId",
                table: "HomeworkSolutions",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_ClassSchoolYearSubjectId",
                table: "Lessons",
                column: "ClassSchoolYearSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxEntries_State",
                table: "OutboxEntries",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassSchoolYearSubjectStudents");

            migrationBuilder.DropTable(
                name: "ClassSchoolYearSubjectTeachers");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "HomeworkReviews");

            migrationBuilder.DropTable(
                name: "OutboxEntries");

            migrationBuilder.DropTable(
                name: "HomeworkSolutions");

            migrationBuilder.DropTable(
                name: "Homeworks");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ClassSchoolYearSubjects");
        }
    }
}
