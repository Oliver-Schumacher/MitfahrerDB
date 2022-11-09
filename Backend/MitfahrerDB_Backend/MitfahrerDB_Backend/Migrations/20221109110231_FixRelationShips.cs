using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MitfahrerDB_Backend.Migrations
{
    public partial class FixRelationShips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Trips_LocationEndId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_LocationStartId",
                table: "Trips");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_LocationEndId",
                table: "Trips",
                column: "LocationEndId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_LocationStartId",
                table: "Trips",
                column: "LocationStartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Trips_LocationEndId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_LocationStartId",
                table: "Trips");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_LocationEndId",
                table: "Trips",
                column: "LocationEndId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_LocationStartId",
                table: "Trips",
                column: "LocationStartId",
                unique: true);
        }
    }
}
