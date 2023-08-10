using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendorMVC.Migrations
{
    /// <inheritdoc />
    public partial class removeCountryStateFromVendor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_Country_CountryID",
                table: "Vendor");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_State_StateID",
                table: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_CountryID",
                table: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_StateID",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "CountryID",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "StateID",
                table: "Vendor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryID",
                table: "Vendor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StateID",
                table: "Vendor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_CountryID",
                table: "Vendor",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_StateID",
                table: "Vendor",
                column: "StateID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_Country_CountryID",
                table: "Vendor",
                column: "CountryID",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_State_StateID",
                table: "Vendor",
                column: "StateID",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
