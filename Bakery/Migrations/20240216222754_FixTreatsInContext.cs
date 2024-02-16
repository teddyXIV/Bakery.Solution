using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bakery.Migrations
{
    public partial class FixTreatsInContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlavorTreats_Treat_TreatId",
                table: "FlavorTreats");

            migrationBuilder.DropForeignKey(
                name: "FK_Treat_AspNetUsers_UserId",
                table: "Treat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Treat",
                table: "Treat");

            migrationBuilder.RenameTable(
                name: "Treat",
                newName: "Treats");

            migrationBuilder.RenameIndex(
                name: "IX_Treat_UserId",
                table: "Treats",
                newName: "IX_Treats_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Treats",
                table: "Treats",
                column: "TreatId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlavorTreats_Treats_TreatId",
                table: "FlavorTreats",
                column: "TreatId",
                principalTable: "Treats",
                principalColumn: "TreatId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Treats_AspNetUsers_UserId",
                table: "Treats",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlavorTreats_Treats_TreatId",
                table: "FlavorTreats");

            migrationBuilder.DropForeignKey(
                name: "FK_Treats_AspNetUsers_UserId",
                table: "Treats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Treats",
                table: "Treats");

            migrationBuilder.RenameTable(
                name: "Treats",
                newName: "Treat");

            migrationBuilder.RenameIndex(
                name: "IX_Treats_UserId",
                table: "Treat",
                newName: "IX_Treat_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Treat",
                table: "Treat",
                column: "TreatId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlavorTreats_Treat_TreatId",
                table: "FlavorTreats",
                column: "TreatId",
                principalTable: "Treat",
                principalColumn: "TreatId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Treat_AspNetUsers_UserId",
                table: "Treat",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
