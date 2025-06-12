using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Heilsunudd.Data.Migrations
{
    /// <inheritdoc />
    public partial class Renamed_AvailableService_to_Service_to_simplify_parsing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationAvailableServices");
            
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_AvailableService_IdService",
                table: "Booking");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AvailableService",
                table: "Service");

            migrationBuilder.RenameTable(
                name: "Service",
                newName: "Service");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Service",
                table: "Service",
                column: "IdService");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Service_IdService",
                table: "Booking",
                column: "IdService",
                principalTable: "Service",
                principalColumn: "IdService",
                onDelete: ReferentialAction.Cascade);
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Service_IdService",
                table: "Booking");
    

            migrationBuilder.DropPrimaryKey(
                name: "PK_Service",
                table: "Service");

            migrationBuilder.RenameTable(
                name: "Service",
                newName: "Service");


            migrationBuilder.AddPrimaryKey(
                name: "PK_AvailableService",
                table: "Service",
                column: "IdService");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_AvailableService_IdService",
                table: "Booking",
                column: "IdService",
                principalTable: "Service",
                principalColumn: "IdService",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
