using Microsoft.EntityFrameworkCore.Migrations;

namespace Reservation.Migrations
{
    public partial class UPReseve : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "confirmation",
                table: "Reserve");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Reserve",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Reserve");

            migrationBuilder.AddColumn<bool>(
                name: "confirmation",
                table: "Reserve",
                type: "tinyint(1)",
                nullable: true);
        }
    }
}
