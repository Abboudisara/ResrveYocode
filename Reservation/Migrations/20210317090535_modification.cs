using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reservation.Migrations
{
    public partial class modification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Conter = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Email = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FullName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    NormalizedEmail = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    PasswordHash = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    PhoneNumber = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SecurityStamp = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UserName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });
        }
    }
}
