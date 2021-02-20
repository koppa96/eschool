using Microsoft.EntityFrameworkCore.Migrations;

namespace ESchool.IdentityProvider.Domain.Migrations
{
    public partial class TenantUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TenantUserRole_TenantUsers_TenantUserId",
                table: "TenantUserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TenantUserRole",
                table: "TenantUserRole");

            migrationBuilder.RenameTable(
                name: "TenantUserRole",
                newName: "TenantUserRoles");

            migrationBuilder.RenameIndex(
                name: "IX_TenantUserRole_TenantUserId",
                table: "TenantUserRoles",
                newName: "IX_TenantUserRoles_TenantUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TenantUserRoles",
                table: "TenantUserRoles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TenantUserRoles_TenantUsers_TenantUserId",
                table: "TenantUserRoles",
                column: "TenantUserId",
                principalTable: "TenantUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TenantUserRoles_TenantUsers_TenantUserId",
                table: "TenantUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TenantUserRoles",
                table: "TenantUserRoles");

            migrationBuilder.RenameTable(
                name: "TenantUserRoles",
                newName: "TenantUserRole");

            migrationBuilder.RenameIndex(
                name: "IX_TenantUserRoles_TenantUserId",
                table: "TenantUserRole",
                newName: "IX_TenantUserRole_TenantUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TenantUserRole",
                table: "TenantUserRole",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TenantUserRole_TenantUsers_TenantUserId",
                table: "TenantUserRole",
                column: "TenantUserId",
                principalTable: "TenantUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
