using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendorMVC.Migrations
{
    /// <inheritdoc />
    public partial class VendorAddressTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vendorAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LandMark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityID = table.Column<int>(type: "int", nullable: false),
                    VendorID = table.Column<int>(type: "int", nullable: false),
                    PinCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vendorAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vendorAddresses_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_vendorAddresses_Vendor_VendorID",
                        column: x => x.VendorID,
                        principalTable: "Vendor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_vendorAddresses_CityID",
                table: "vendorAddresses",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_vendorAddresses_VendorID",
                table: "vendorAddresses",
                column: "VendorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vendorAddresses");
        }
    }
}
