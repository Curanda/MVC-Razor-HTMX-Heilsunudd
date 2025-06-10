using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Heilsunudd.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedUniqueConstraintsEditableConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Status_StatusName",
                table: "Status",
                column: "StatusName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_LocationName",
                table: "Location",
                column: "LocationName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactInformation_Kennitala",
                table: "ContactInformation",
                column: "Kennitala",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogPost_Title",
                table: "BlogPost",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategory_CategoryName",
                table: "BlogCategory",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AvailableService_ServiceName",
                table: "AvailableService",
                column: "ServiceName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AboutUs_Title",
                table: "AboutUs",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Status_StatusName",
                table: "Status");

            migrationBuilder.DropIndex(
                name: "IX_Location_LocationName",
                table: "Location");

            migrationBuilder.DropIndex(
                name: "IX_ContactInformation_Kennitala",
                table: "ContactInformation");

            migrationBuilder.DropIndex(
                name: "IX_BlogPost_Title",
                table: "BlogPost");

            migrationBuilder.DropIndex(
                name: "IX_BlogCategory_CategoryName",
                table: "BlogCategory");

            migrationBuilder.DropIndex(
                name: "IX_AvailableService_ServiceName",
                table: "AvailableService");

            migrationBuilder.DropIndex(
                name: "IX_AboutUs_Title",
                table: "AboutUs");
        }
    }
}
