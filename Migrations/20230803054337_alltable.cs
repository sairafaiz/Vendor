using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendorMVC.Migrations
{
    /// <inheritdoc />
    public partial class alltable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imageurl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryID = table.Column<int>(type: "int", nullable: false),
                    StateID = table.Column<int>(type: "int", nullable: false),
                    CityID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vendor_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vendor_Country_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Country",
                        principalColumn: "Id"
                       // ,
                      //  onDelete: ReferentialAction.Cascade
                        );
                    table.ForeignKey(
                        name: "FK_Vendor_State_StateID",
                        column: x => x.StateID,
                        principalTable: "State",
                        principalColumn: "Id"
                        //,
                        //onDelete: ReferentialAction.Cascade
                        );
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_StateID",
                table: "Cities",
                column: "StateID");


            migrationBuilder.CreateIndex(
                name: "IX_Vendor_CityID",
                table: "Vendor",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_CountryID",
                table: "Vendor",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_StateID",
                table: "Vendor",
                column: "StateID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vendor");

          

        }
    }
}
