using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ESchool.IdentityProvider.Domain.Migrations
{
    public partial class DefaultTenant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DefaultTenantId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DefaultTenantId",
                table: "AspNetUsers",
                column: "DefaultTenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Tenants_DefaultTenantId",
                table: "AspNetUsers",
                column: "DefaultTenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Tenants_DefaultTenantId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DefaultTenantId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DefaultTenantId",
                table: "AspNetUsers");
        }
    }
}
