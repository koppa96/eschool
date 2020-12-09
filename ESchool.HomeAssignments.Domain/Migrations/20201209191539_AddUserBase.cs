using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ESchool.HomeAssignments.Domain.Migrations
{
    public partial class AddUserBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupStudents_Students_StudentId",
                table: "GroupStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTeachers_Teachers_TeacherId",
                table: "GroupTeachers");

            migrationBuilder.DropForeignKey(
                name: "FK_HomeWorkReviews_Teachers_ReviewerId",
                table: "HomeWorkReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_HomeWorkSolutions_Students_StudentId",
                table: "HomeWorkSolutions");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers");

            migrationBuilder.RenameTable(
                name: "Teachers",
                newName: "UserBase");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "UserBase",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "UserBase",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UserBase",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBase",
                table: "UserBase",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupStudents_UserBase_StudentId",
                table: "GroupStudents",
                column: "StudentId",
                principalTable: "UserBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTeachers_UserBase_TeacherId",
                table: "GroupTeachers",
                column: "TeacherId",
                principalTable: "UserBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HomeWorkReviews_UserBase_ReviewerId",
                table: "HomeWorkReviews",
                column: "ReviewerId",
                principalTable: "UserBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HomeWorkSolutions_UserBase_StudentId",
                table: "HomeWorkSolutions",
                column: "StudentId",
                principalTable: "UserBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupStudents_UserBase_StudentId",
                table: "GroupStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTeachers_UserBase_TeacherId",
                table: "GroupTeachers");

            migrationBuilder.DropForeignKey(
                name: "FK_HomeWorkReviews_UserBase_ReviewerId",
                table: "HomeWorkReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_HomeWorkSolutions_UserBase_StudentId",
                table: "HomeWorkSolutions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBase",
                table: "UserBase");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "UserBase");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "UserBase");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserBase");

            migrationBuilder.RenameTable(
                name: "UserBase",
                newName: "Teachers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupStudents_Students_StudentId",
                table: "GroupStudents",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTeachers_Teachers_TeacherId",
                table: "GroupTeachers",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HomeWorkReviews_Teachers_ReviewerId",
                table: "HomeWorkReviews",
                column: "ReviewerId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HomeWorkSolutions_Students_StudentId",
                table: "HomeWorkSolutions",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
