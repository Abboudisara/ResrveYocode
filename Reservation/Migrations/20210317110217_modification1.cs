using Microsoft.EntityFrameworkCore.Migrations;

namespace Reservation.Migrations
{
    public partial class modification1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Conter",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "Counter",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Counter",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "Conter",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
