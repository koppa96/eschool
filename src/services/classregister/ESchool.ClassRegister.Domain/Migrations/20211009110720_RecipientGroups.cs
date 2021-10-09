using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ESchool.ClassRegister.Domain.Migrations
{
    public partial class RecipientGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecipientGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipientGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipientGroups_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipientGroupMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipientGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    MemberId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipientGroupMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipientGroupMembers_RecipientGroups_RecipientGroupId",
                        column: x => x.RecipientGroupId,
                        principalTable: "RecipientGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipientGroupMembers_Users_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipientGroupMembers_MemberId",
                table: "RecipientGroupMembers",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipientGroupMembers_RecipientGroupId",
                table: "RecipientGroupMembers",
                column: "RecipientGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipientGroups_UserId",
                table: "RecipientGroups",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipientGroupMembers");

            migrationBuilder.DropTable(
                name: "RecipientGroups");
        }
    }
}
