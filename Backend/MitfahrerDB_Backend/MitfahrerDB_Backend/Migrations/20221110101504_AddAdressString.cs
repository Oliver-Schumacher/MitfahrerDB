using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MitfahrerDB_Backend.Migrations
{
    public partial class AddAdressString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Trips",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Trips");
        }
    }
}
