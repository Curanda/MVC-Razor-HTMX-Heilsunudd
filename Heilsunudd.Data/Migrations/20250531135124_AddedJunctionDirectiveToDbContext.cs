using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Heilsunudd.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedJunctionDirectiveToDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvailableServiceLocation_AvailableService_AvailableServicesIdService",
                table: "AvailableServiceLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_AvailableServiceLocation_Location_LocationsIdLocation",
                table: "AvailableServiceLocation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AvailableServiceLocation",
                table: "AvailableServiceLocation");

            migrationBuilder.RenameTable(
                name: "AvailableServiceLocation",
                newName: "LocationAvailableServices");

            migrationBuilder.RenameIndex(
                name: "IX_AvailableServiceLocation_LocationsIdLocation",
                table: "LocationAvailableServices",
                newName: "IX_LocationAvailableServices_LocationsIdLocation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocationAvailableServices",
                table: "LocationAvailableServices",
                columns: new[] { "AvailableServicesIdService", "LocationsIdLocation" });

            migrationBuilder.AddForeignKey(
                name: "FK_LocationAvailableServices_AvailableService_AvailableServicesIdService",
                table: "LocationAvailableServices",
                column: "AvailableServicesIdService",
                principalTable: "AvailableService",
                principalColumn: "IdService",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationAvailableServices_Location_LocationsIdLocation",
                table: "LocationAvailableServices",
                column: "LocationsIdLocation",
                principalTable: "Location",
                principalColumn: "IdLocation",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationAvailableServices_AvailableService_AvailableServicesIdService",
                table: "LocationAvailableServices");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationAvailableServices_Location_LocationsIdLocation",
                table: "LocationAvailableServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationAvailableServices",
                table: "LocationAvailableServices");

            migrationBuilder.RenameTable(
                name: "LocationAvailableServices",
                newName: "AvailableServiceLocation");

            migrationBuilder.RenameIndex(
                name: "IX_LocationAvailableServices_LocationsIdLocation",
                table: "AvailableServiceLocation",
                newName: "IX_AvailableServiceLocation_LocationsIdLocation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvailableServiceLocation",
                table: "AvailableServiceLocation",
                columns: new[] { "AvailableServicesIdService", "LocationsIdLocation" });

            migrationBuilder.AddForeignKey(
                name: "FK_AvailableServiceLocation_AvailableService_AvailableServicesIdService",
                table: "AvailableServiceLocation",
                column: "AvailableServicesIdService",
                principalTable: "AvailableService",
                principalColumn: "IdService",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AvailableServiceLocation_Location_LocationsIdLocation",
                table: "AvailableServiceLocation",
                column: "LocationsIdLocation",
                principalTable: "Location",
                principalColumn: "IdLocation",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
