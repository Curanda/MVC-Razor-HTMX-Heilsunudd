using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Heilsunudd.Data.Migrations
{
    /// <inheritdoc />
    public partial class SwitchedLocationIdToIdLocationInCalendar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendar_Location_LocationId",
                table: "Calendar");

            migrationBuilder.DropIndex(
                name: "IX_Calendar_LocationId_StartTime",
                table: "Calendar");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Calendar",
                newName: "IdLocation");

            migrationBuilder.CreateIndex(
                name: "IX_Calendar_IdLocation",
                table: "Calendar",
                column: "IdLocation");

            migrationBuilder.AddForeignKey(
                name: "FK_Calendar_Location_IdLocation",
                table: "Calendar",
                column: "IdLocation",
                principalTable: "Location",
                principalColumn: "IdLocation",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendar_Location_IdLocation",
                table: "Calendar");

            migrationBuilder.DropIndex(
                name: "IX_Calendar_IdLocation",
                table: "Calendar");

            migrationBuilder.RenameColumn(
                name: "IdLocation",
                table: "Calendar",
                newName: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Calendar_LocationId_StartTime",
                table: "Calendar",
                columns: new[] { "LocationId", "StartTime" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Calendar_Location_LocationId",
                table: "Calendar",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "IdLocation",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
