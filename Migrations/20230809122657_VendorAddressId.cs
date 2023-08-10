using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendorMVC.Migrations
{
    /// <inheritdoc />
    public partial class VendorAddressId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "vendorAddresses",
                newName: "AddressID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddressID",
                table: "vendorAddresses",
                newName: "Id");
        }
    }
}
