using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Heilsunudd.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedDateTimeInCalendar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Calendar",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "EndTime",
                table: "Calendar",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");
        }
    }
}
