using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ESchool.ClassRegister.Domain.Migrations
{
    public partial class HeadTeachers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupTeachers_Groups_GroupId",
                table: "GroupTeachers");

            migrationBuilder.DropIndex(
                name: "IX_GroupTeachers_GroupId",
                table: "GroupTeachers");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Lessons");

            migrationBuilder.AddColumn<Guid>(
                name: "CurrentClassId",
                table: "UserBase",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WrittenIn",
                table: "SmallGrades",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Canceled",
                table: "Lessons",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Lessons",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LessonNumber",
                table: "Lessons",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Lessons",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "HomeWorks",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HeadTeacherId",
                table: "Classes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserBase_CurrentClassId",
                table: "UserBase",
                column: "CurrentClassId",
                unique: true,
                filter: "[CurrentClassId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_HeadTeacherId",
                table: "Classes",
                column: "HeadTeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_UserBase_HeadTeacherId",
                table: "Classes",
                column: "HeadTeacherId",
                principalTable: "UserBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTeachers_Groups_TeacherId",
                table: "GroupTeachers",
                column: "TeacherId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBase_Classes_CurrentClassId",
                table: "UserBase",
                column: "CurrentClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_UserBase_HeadTeacherId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTeachers_Groups_TeacherId",
                table: "GroupTeachers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBase_Classes_CurrentClassId",
                table: "UserBase");

            migrationBuilder.DropIndex(
                name: "IX_UserBase_CurrentClassId",
                table: "UserBase");

            migrationBuilder.DropIndex(
                name: "IX_Classes_HeadTeacherId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "CurrentClassId",
                table: "UserBase");

            migrationBuilder.DropColumn(
                name: "WrittenIn",
                table: "SmallGrades");

            migrationBuilder.DropColumn(
                name: "Canceled",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "LessonNumber",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "HomeWorks");

            migrationBuilder.DropColumn(
                name: "HeadTeacherId",
                table: "Classes");

            migrationBuilder.AddColumn<Guid>(
                name: "RoomId",
                table: "Lessons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_GroupTeachers_GroupId",
                table: "GroupTeachers",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTeachers_Groups_GroupId",
                table: "GroupTeachers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
