using Microsoft.EntityFrameworkCore.Migrations;

namespace Reservation.Migrations
{
    public partial class modificationReserve : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reserve_AspNetUsers_conterId",
                table: "Reserve");

            migrationBuilder.DropIndex(
                name: "IX_Reserve_conterId",
                table: "Reserve");

            migrationBuilder.DropColumn(
                name: "conterId",
                table: "Reserve");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "conterId",
                table: "Reserve",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reserve_conterId",
                table: "Reserve",
                column: "conterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reserve_AspNetUsers_conterId",
                table: "Reserve",
                column: "conterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
