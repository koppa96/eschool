using Microsoft.EntityFrameworkCore.Migrations;

namespace ESchool.ClassRegister.Domain.Migrations
{
    public partial class StudentParentTableRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentParent_UserRoles_ParentId",
                table: "StudentParent");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentParent_UserRoles_StudentId",
                table: "StudentParent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentParent",
                table: "StudentParent");

            migrationBuilder.RenameTable(
                name: "StudentParent",
                newName: "StudentParents");

            migrationBuilder.RenameIndex(
                name: "IX_StudentParent_StudentId",
                table: "StudentParents",
                newName: "IX_StudentParents_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentParent_ParentId",
                table: "StudentParents",
                newName: "IX_StudentParents_ParentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentParents",
                table: "StudentParents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentParents_UserRoles_ParentId",
                table: "StudentParents",
                column: "ParentId",
                principalTable: "UserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentParents_UserRoles_StudentId",
                table: "StudentParents",
                column: "StudentId",
                principalTable: "UserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentParents_UserRoles_ParentId",
                table: "StudentParents");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentParents_UserRoles_StudentId",
                table: "StudentParents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentParents",
                table: "StudentParents");

            migrationBuilder.RenameTable(
                name: "StudentParents",
                newName: "StudentParent");

            migrationBuilder.RenameIndex(
                name: "IX_StudentParents_StudentId",
                table: "StudentParent",
                newName: "IX_StudentParent_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentParents_ParentId",
                table: "StudentParent",
                newName: "IX_StudentParent_ParentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentParent",
                table: "StudentParent",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentParent_UserRoles_ParentId",
                table: "StudentParent",
                column: "ParentId",
                principalTable: "UserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentParent_UserRoles_StudentId",
                table: "StudentParent",
                column: "StudentId",
                principalTable: "UserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
