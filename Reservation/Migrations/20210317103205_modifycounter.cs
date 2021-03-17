using Microsoft.EntityFrameworkCore.Migrations;

namespace Reservation.Migrations
{
    public partial class modifycounter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "conterId",
                table: "Reserve",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Conter",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Conter",
                table: "AspNetUsers",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
