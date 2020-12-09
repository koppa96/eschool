using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ESchool.Testing.Domain.Migrations
{
    public partial class AddUserBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Students_StudentId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Students_StudentId1",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupStudent_Students_StudentId",
                table: "GroupStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTeacher_Teachers_TeacherId",
                table: "GroupTeacher");

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
                name: "FK_Answers_UserBase_StudentId",
                table: "Answers",
                column: "StudentId",
                principalTable: "UserBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_UserBase_StudentId1",
                table: "Answers",
                column: "StudentId1",
                principalTable: "UserBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupStudent_UserBase_StudentId",
                table: "GroupStudent",
                column: "StudentId",
                principalTable: "UserBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTeacher_UserBase_TeacherId",
                table: "GroupTeacher",
                column: "TeacherId",
                principalTable: "UserBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_UserBase_StudentId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_UserBase_StudentId1",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupStudent_UserBase_StudentId",
                table: "GroupStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTeacher_UserBase_TeacherId",
                table: "GroupTeacher");

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
                name: "FK_Answers_Students_StudentId",
                table: "Answers",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Students_StudentId1",
                table: "Answers",
                column: "StudentId1",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupStudent_Students_StudentId",
                table: "GroupStudent",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTeacher_Teachers_TeacherId",
                table: "GroupTeacher",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
