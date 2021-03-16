using Microsoft.EntityFrameworkCore.Migrations;

namespace Reservation.Migrations
{
    public partial class userid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reserve_user_User_id",
                table: "Reserve");

            migrationBuilder.AddForeignKey(
                name: "FK_Reserve_AspNetUsers_User_id",
                table: "Reserve",
                column: "User_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reserve_AspNetUsers_User_id",
                table: "Reserve");

            migrationBuilder.AddForeignKey(
                name: "FK_Reserve_user_User_id",
                table: "Reserve",
                column: "User_id",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
