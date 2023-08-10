using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendorMVC.Migrations
{
    /// <inheritdoc />
    public partial class VendorAddressTableupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vendorAddresses_Cities_CityID",
                table: "vendorAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_vendorAddresses_Vendor_VendorID",
                table: "vendorAddresses");

            migrationBuilder.AlterColumn<int>(
                name: "VendorID",
                table: "vendorAddresses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CityID",
                table: "vendorAddresses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_vendorAddresses_Cities_CityID",
                table: "vendorAddresses",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_vendorAddresses_Vendor_VendorID",
                table: "vendorAddresses",
                column: "VendorID",
                principalTable: "Vendor",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vendorAddresses_Cities_CityID",
                table: "vendorAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_vendorAddresses_Vendor_VendorID",
                table: "vendorAddresses");

            migrationBuilder.AlterColumn<int>(
                name: "VendorID",
                table: "vendorAddresses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CityID",
                table: "vendorAddresses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_vendorAddresses_Cities_CityID",
                table: "vendorAddresses",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_vendorAddresses_Vendor_VendorID",
                table: "vendorAddresses",
                column: "VendorID",
                principalTable: "Vendor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
