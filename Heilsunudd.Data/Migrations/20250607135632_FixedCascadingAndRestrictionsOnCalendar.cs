using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Heilsunudd.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedCascadingAndRestrictionsOnCalendar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Calendar_LocationName_StartTime",
                table: "Calendar");

            migrationBuilder.DropColumn(
                name: "LocationName",
                table: "Calendar");

            migrationBuilder.DropColumn(
                name: "StatusName",
                table: "Calendar");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Calendar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Calendar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    IdBooking = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Kennitala = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    BookingTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.IdBooking);
                    table.ForeignKey(
                        name: "FK_Booking_AvailableService_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "AvailableService",
                        principalColumn: "IdService",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Booking_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "IdLocation",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Booking_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "IdStatus",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calendar_IdBooking",
                table: "Calendar",
                column: "IdBooking");

            migrationBuilder.CreateIndex(
                name: "IX_Calendar_LocationId_StartTime",
                table: "Calendar",
                columns: new[] { "LocationId", "StartTime" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Calendar_StatusId",
                table: "Calendar",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_LocationId",
                table: "Booking",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ServiceId",
                table: "Booking",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_StatusId",
                table: "Booking",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Calendar_Booking_IdBooking",
                table: "Calendar",
                column: "IdBooking",
                principalTable: "Booking",
                principalColumn: "IdBooking",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Calendar_Location_LocationId",
                table: "Calendar",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "IdLocation",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Calendar_Status_StatusId",
                table: "Calendar",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "IdStatus",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendar_Booking_IdBooking",
                table: "Calendar");

            migrationBuilder.DropForeignKey(
                name: "FK_Calendar_Location_LocationId",
                table: "Calendar");

            migrationBuilder.DropForeignKey(
                name: "FK_Calendar_Status_StatusId",
                table: "Calendar");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Calendar_IdBooking",
                table: "Calendar");

            migrationBuilder.DropIndex(
                name: "IX_Calendar_LocationId_StartTime",
                table: "Calendar");

            migrationBuilder.DropIndex(
                name: "IX_Calendar_StatusId",
                table: "Calendar");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Calendar");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Calendar");

            migrationBuilder.AddColumn<string>(
                name: "LocationName",
                table: "Calendar",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StatusName",
                table: "Calendar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Calendar_LocationName_StartTime",
                table: "Calendar",
                columns: new[] { "LocationName", "StartTime" },
                unique: true);
        }
    }
}
