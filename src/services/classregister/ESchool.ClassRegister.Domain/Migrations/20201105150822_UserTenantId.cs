using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ESchool.ClassRegister.Domain.Migrations
{
    public partial class UserTenantId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_UserBase_StudentId",
                table: "Absences");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_UserBase_HeadTeacherId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_UserBase_StudentId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_UserBase_TeacherId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupStudents_UserBase_StudentId",
                table: "GroupStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTeachers_UserBase_TeacherId",
                table: "GroupTeachers");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_UserBase_SenderUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_SmallGrades_UserBase_StudentId",
                table: "SmallGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_SmallGrades_UserBase_TeacherId",
                table: "SmallGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentParent_UserBase_ParentId",
                table: "StudentParent");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentParent_UserBase_StudentId",
                table: "StudentParent");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBase_Classes_ClassId",
                table: "UserBase");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBase_Classes_CurrentClassId",
                table: "UserBase");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMessages_UserBase_UserId",
                table: "UserMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBase",
                table: "UserBase");

            migrationBuilder.RenameTable(
                name: "UserBase",
                newName: "UserBases");

            migrationBuilder.RenameIndex(
                name: "IX_UserBase_CurrentClassId",
                table: "UserBases",
                newName: "IX_UserBases_CurrentClassId");

            migrationBuilder.RenameIndex(
                name: "IX_UserBase_ClassId",
                table: "UserBases",
                newName: "IX_UserBases_ClassId");

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "UserBases",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UserBases",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBases",
                table: "UserBases",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_UserBases_StudentId",
                table: "Absences",
                column: "StudentId",
                principalTable: "UserBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_UserBases_HeadTeacherId",
                table: "Classes",
                column: "HeadTeacherId",
                principalTable: "UserBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_UserBases_StudentId",
                table: "Grades",
                column: "StudentId",
                principalTable: "UserBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_UserBases_TeacherId",
                table: "Grades",
                column: "TeacherId",
                principalTable: "UserBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupStudents_UserBases_StudentId",
                table: "GroupStudents",
                column: "StudentId",
                principalTable: "UserBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTeachers_UserBases_TeacherId",
                table: "GroupTeachers",
                column: "TeacherId",
                principalTable: "UserBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_UserBases_SenderUserId",
                table: "Messages",
                column: "SenderUserId",
                principalTable: "UserBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SmallGrades_UserBases_StudentId",
                table: "SmallGrades",
                column: "StudentId",
                principalTable: "UserBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SmallGrades_UserBases_TeacherId",
                table: "SmallGrades",
                column: "TeacherId",
                principalTable: "UserBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentParent_UserBases_ParentId",
                table: "StudentParent",
                column: "ParentId",
                principalTable: "UserBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentParent_UserBases_StudentId",
                table: "StudentParent",
                column: "StudentId",
                principalTable: "UserBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBases_Classes_ClassId",
                table: "UserBases",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBases_Classes_CurrentClassId",
                table: "UserBases",
                column: "CurrentClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessages_UserBases_UserId",
                table: "UserMessages",
                column: "UserId",
                principalTable: "UserBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_UserBases_StudentId",
                table: "Absences");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_UserBases_HeadTeacherId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_UserBases_StudentId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_UserBases_TeacherId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupStudents_UserBases_StudentId",
                table: "GroupStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupTeachers_UserBases_TeacherId",
                table: "GroupTeachers");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_UserBases_SenderUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_SmallGrades_UserBases_StudentId",
                table: "SmallGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_SmallGrades_UserBases_TeacherId",
                table: "SmallGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentParent_UserBases_ParentId",
                table: "StudentParent");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentParent_UserBases_StudentId",
                table: "StudentParent");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBases_Classes_ClassId",
                table: "UserBases");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBases_Classes_CurrentClassId",
                table: "UserBases");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMessages_UserBases_UserId",
                table: "UserMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBases",
                table: "UserBases");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserBases");

            migrationBuilder.RenameTable(
                name: "UserBases",
                newName: "UserBase");

            migrationBuilder.RenameIndex(
                name: "IX_UserBases_CurrentClassId",
                table: "UserBase",
                newName: "IX_UserBase_CurrentClassId");

            migrationBuilder.RenameIndex(
                name: "IX_UserBases_ClassId",
                table: "UserBase",
                newName: "IX_UserBase_ClassId");

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "UserBase",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBase",
                table: "UserBase",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_UserBase_StudentId",
                table: "Absences",
                column: "StudentId",
                principalTable: "UserBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_UserBase_HeadTeacherId",
                table: "Classes",
                column: "HeadTeacherId",
                principalTable: "UserBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_UserBase_StudentId",
                table: "Grades",
                column: "StudentId",
                principalTable: "UserBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_UserBase_TeacherId",
                table: "Grades",
                column: "TeacherId",
                principalTable: "UserBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Messages_UserBase_SenderUserId",
                table: "Messages",
                column: "SenderUserId",
                principalTable: "UserBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SmallGrades_UserBase_StudentId",
                table: "SmallGrades",
                column: "StudentId",
                principalTable: "UserBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SmallGrades_UserBase_TeacherId",
                table: "SmallGrades",
                column: "TeacherId",
                principalTable: "UserBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentParent_UserBase_ParentId",
                table: "StudentParent",
                column: "ParentId",
                principalTable: "UserBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentParent_UserBase_StudentId",
                table: "StudentParent",
                column: "StudentId",
                principalTable: "UserBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBase_Classes_ClassId",
                table: "UserBase",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBase_Classes_CurrentClassId",
                table: "UserBase",
                column: "CurrentClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessages_UserBase_UserId",
                table: "UserMessages",
                column: "UserId",
                principalTable: "UserBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
