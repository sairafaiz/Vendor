using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendorMVC.Migrations
{
    /// <inheritdoc />
    public partial class VendorAddressTableupdatesession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Session",
                table: "vendorAddresses",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Session",
                table: "vendorAddresses");
        }
    }
}
