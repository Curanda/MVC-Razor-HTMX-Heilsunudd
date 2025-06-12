using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Heilsunudd.Data.Migrations
{
    /// <inheritdoc />
    public partial class RollingBackToSimpleVirtualIcollectionOfServicesOnLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationService");

            migrationBuilder.AddColumn<int>(
                name: "LocationIdLocation",
                table: "Service",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AvailableService_LocationIdLocation",
                table: "Service",
                column: "LocationIdLocation");

            migrationBuilder.AddForeignKey(
                name: "FK_AvailableService_Location_LocationIdLocation",
                table: "Service",
                column: "LocationIdLocation",
                principalTable: "Location",
                principalColumn: "IdLocation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvailableService_Location_LocationIdLocation",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_AvailableService_LocationIdLocation",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "LocationIdLocation",
                table: "Service");

            migrationBuilder.CreateTable(
                name: "LocationService",
                columns: table => new
                {
                    IdLocation = table.Column<int>(type: "int", nullable: false),
                    IdService = table.Column<int>(type: "int", nullable: false),
                    IdAvailableService = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationService", x => new { x.IdLocation, x.IdService });
                    table.ForeignKey(
                        name: "FK_LocationService_AvailableService_IdService",
                        column: x => x.IdService,
                        principalTable: "Service",
                        principalColumn: "IdService",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationService_Location_IdLocation",
                        column: x => x.IdLocation,
                        principalTable: "Location",
                        principalColumn: "IdLocation",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocationService_IdService",
                table: "LocationService",
                column: "IdService");
        }
    }
}
