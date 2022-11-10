using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MitfahrerDB_Backend.Migrations
{
    public partial class AddWeekDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WeekDay",
                table: "Trips",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeekDay",
                table: "Trips");
        }
    }
}
