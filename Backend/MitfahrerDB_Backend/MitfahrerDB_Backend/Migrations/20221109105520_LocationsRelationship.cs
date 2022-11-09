using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MitfahrerDB_Backend.Migrations
{
    public partial class LocationsRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTrips_Trips_TripId",
                table: "UserTrips");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTrips_Users_UserId",
                table: "UserTrips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_LocationEndId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_LocationStartId",
                table: "Trips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTrips",
                table: "UserTrips");

            migrationBuilder.RenameTable(
                name: "UserTrips",
                newName: "UserTips");

            migrationBuilder.RenameIndex(
                name: "IX_UserTrips_UserId",
                table: "UserTips",
                newName: "IX_UserTips_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserTrips_TripId",
                table: "UserTips",
                newName: "IX_UserTips_TripId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTips",
                table: "UserTips",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_UserTips_Trips_TripId",
                table: "UserTips",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTips_Users_UserId",
                table: "UserTips",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTips_Trips_TripId",
                table: "UserTips");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTips_Users_UserId",
                table: "UserTips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_LocationEndId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_LocationStartId",
                table: "Trips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTips",
                table: "UserTips");

            migrationBuilder.RenameTable(
                name: "UserTips",
                newName: "UserTrips");

            migrationBuilder.RenameIndex(
                name: "IX_UserTips_UserId",
                table: "UserTrips",
                newName: "IX_UserTrips_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserTips_TripId",
                table: "UserTrips",
                newName: "IX_UserTrips_TripId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTrips",
                table: "UserTrips",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_LocationEndId",
                table: "Trips",
                column: "LocationEndId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_LocationStartId",
                table: "Trips",
                column: "LocationStartId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTrips_Trips_TripId",
                table: "UserTrips",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTrips_Users_UserId",
                table: "UserTrips",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
