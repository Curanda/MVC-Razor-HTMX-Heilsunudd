using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Heilsunudd.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fixed_Manual_delete_of_LocationService_Junction_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
{
    
    migrationBuilder.DropForeignKey(
        name: "FK_Calendar_Status_StatusId",
        table: "Calendar");

    migrationBuilder.RenameIndex(
        name: "IX_Calendar_StatusId",
        table: "Calendar",
        newName: "IX_Calendar_IdStatus");
    
        migrationBuilder.CreateTable(
            name: "LocationServices",
            columns: table => new
            {
                LocationsIdLocation = table.Column<int>(type: "int", nullable: false),
                ServicesIdService = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LocationServices", x => new { x.LocationsIdLocation, x.ServicesIdService });
                table.ForeignKey(
                    name: "FK_LocationServices_Location_LocationsIdLocation",
                    column: x => x.LocationsIdLocation,
                    principalTable: "Location",
                    principalColumn: "IdLocation",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_LocationServices_Service_ServicesIdService",
                    column: x => x.ServicesIdService,
                    principalTable: "Service",
                    principalColumn: "IdService",
                    onDelete: ReferentialAction.Cascade);
            });

            migrationBuilder.CreateIndex(
                name: "IX_LocationServices_ServicesIdService",
                table: "LocationServices",
                column: "ServicesIdService");

            // Add back the Calendar foreign key
            migrationBuilder.AddForeignKey(
                name: "FK_Calendar_Status_IdStatus",
                table: "Calendar",
                column: "IdStatus",
                principalTable: "Status",
                principalColumn: "IdStatus",
                onDelete: ReferentialAction.Restrict);
        }

    }
}
