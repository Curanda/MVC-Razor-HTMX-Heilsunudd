using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Heilsunudd.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fixed_FK_naming_conventions_in_Booking_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_AvailableService_ServiceId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Location_LocationId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Status_StatusId",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Booking",
                newName: "IdStatus");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "Booking",
                newName: "IdService");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Booking",
                newName: "IdLocation");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_StatusId",
                table: "Booking",
                newName: "IX_Booking_IdStatus");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_ServiceId",
                table: "Booking",
                newName: "IX_Booking_IdService");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_LocationId",
                table: "Booking",
                newName: "IX_Booking_IdLocation");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_AvailableService_IdService",
                table: "Booking",
                column: "IdService",
                principalTable: "AvailableService",
                principalColumn: "IdService",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Location_IdLocation",
                table: "Booking",
                column: "IdLocation",
                principalTable: "Location",
                principalColumn: "IdLocation",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Status_IdStatus",
                table: "Booking",
                column: "IdStatus",
                principalTable: "Status",
                principalColumn: "IdStatus",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_AvailableService_IdService",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Location_IdLocation",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Status_IdStatus",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "IdStatus",
                table: "Booking",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "IdService",
                table: "Booking",
                newName: "ServiceId");

            migrationBuilder.RenameColumn(
                name: "IdLocation",
                table: "Booking",
                newName: "LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_IdStatus",
                table: "Booking",
                newName: "IX_Booking_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_IdService",
                table: "Booking",
                newName: "IX_Booking_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_IdLocation",
                table: "Booking",
                newName: "IX_Booking_LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_AvailableService_ServiceId",
                table: "Booking",
                column: "ServiceId",
                principalTable: "AvailableService",
                principalColumn: "IdService",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Location_LocationId",
                table: "Booking",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "IdLocation",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Status_StatusId",
                table: "Booking",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "IdStatus",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
