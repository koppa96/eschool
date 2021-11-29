using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ESchool.Testing.Domain.Migrations
{
    public partial class ClassRegisterEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "ClassSchoolYearSubjects");

            migrationBuilder.DropColumn(
                name: "SchoolYearId",
                table: "ClassSchoolYearSubjects");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "ClassSchoolYearSubjects");

            migrationBuilder.AddColumn<Guid>(
                name: "Class_Id",
                table: "ClassSchoolYearSubjects",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Class_Name",
                table: "ClassSchoolYearSubjects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolYear_Id",
                table: "ClassSchoolYearSubjects",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SchoolYear_Name",
                table: "ClassSchoolYearSubjects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Subject_Id",
                table: "ClassSchoolYearSubjects",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subject_Name",
                table: "ClassSchoolYearSubjects",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Class_Id",
                table: "ClassSchoolYearSubjects");

            migrationBuilder.DropColumn(
                name: "Class_Name",
                table: "ClassSchoolYearSubjects");

            migrationBuilder.DropColumn(
                name: "SchoolYear_Id",
                table: "ClassSchoolYearSubjects");

            migrationBuilder.DropColumn(
                name: "SchoolYear_Name",
                table: "ClassSchoolYearSubjects");

            migrationBuilder.DropColumn(
                name: "Subject_Id",
                table: "ClassSchoolYearSubjects");

            migrationBuilder.DropColumn(
                name: "Subject_Name",
                table: "ClassSchoolYearSubjects");

            migrationBuilder.AddColumn<Guid>(
                name: "ClassId",
                table: "ClassSchoolYearSubjects",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolYearId",
                table: "ClassSchoolYearSubjects",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectId",
                table: "ClassSchoolYearSubjects",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
