using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Heilsunudd.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedOneToManyLocationService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvailableService_Location_LocationIdLocation",
                table: "AvailableService");

            migrationBuilder.DropIndex(
                name: "IX_AvailableService_LocationIdLocation",
                table: "AvailableService");

            migrationBuilder.DropColumn(
                name: "LocationIdLocation",
                table: "AvailableService");

            migrationBuilder.CreateTable(
                name: "AvailableServiceLocation",
                columns: table => new
                {
                    AvailableServicesIdService = table.Column<int>(type: "int", nullable: false),
                    LocationsIdLocation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableServiceLocation", x => new { x.AvailableServicesIdService, x.LocationsIdLocation });
                    table.ForeignKey(
                        name: "FK_AvailableServiceLocation_AvailableService_AvailableServicesIdService",
                        column: x => x.AvailableServicesIdService,
                        principalTable: "AvailableService",
                        principalColumn: "IdService",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AvailableServiceLocation_Location_LocationsIdLocation",
                        column: x => x.LocationsIdLocation,
                        principalTable: "Location",
                        principalColumn: "IdLocation",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvailableServiceLocation_LocationsIdLocation",
                table: "AvailableServiceLocation",
                column: "LocationsIdLocation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvailableServiceLocation");

            migrationBuilder.AddColumn<int>(
                name: "LocationIdLocation",
                table: "AvailableService",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AvailableService_LocationIdLocation",
                table: "AvailableService",
                column: "LocationIdLocation");

            migrationBuilder.AddForeignKey(
                name: "FK_AvailableService_Location_LocationIdLocation",
                table: "AvailableService",
                column: "LocationIdLocation",
                principalTable: "Location",
                principalColumn: "IdLocation");
        }
    }
}
