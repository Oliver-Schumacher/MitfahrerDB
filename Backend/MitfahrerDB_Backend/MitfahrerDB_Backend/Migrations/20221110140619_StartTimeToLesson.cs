using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MitfahrerDB_Backend.Migrations
{
    public partial class StartTimeToLesson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Trips");

            migrationBuilder.AddColumn<int>(
                name: "Lesson",
                table: "Trips",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lesson",
                table: "Trips");

            migrationBuilder.AddColumn<string>(
                name: "StartTime",
                table: "Trips",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
