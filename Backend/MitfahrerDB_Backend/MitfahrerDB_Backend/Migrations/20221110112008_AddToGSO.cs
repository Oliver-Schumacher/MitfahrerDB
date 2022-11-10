using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MitfahrerDB_Backend.Migrations
{
    public partial class AddToGSO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adress",
                table: "Trips",
                newName: "Address");

            migrationBuilder.AddColumn<int>(
                name: "ToGSO",
                table: "Trips",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToGSO",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Trips",
                newName: "Adress");
        }
    }
}
