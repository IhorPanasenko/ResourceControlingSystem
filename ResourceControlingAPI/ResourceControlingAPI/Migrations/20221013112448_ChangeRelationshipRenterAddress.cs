using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResourceControlingAPI.Migrations
{
    public partial class ChangeRelationshipRenterAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressRenter");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Renters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Renters_AddressId",
                table: "Renters",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Renters_Addresses_AddressId",
                table: "Renters",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Renters_Addresses_AddressId",
                table: "Renters");

            migrationBuilder.DropIndex(
                name: "IX_Renters_AddressId",
                table: "Renters");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Renters");

            migrationBuilder.CreateTable(
                name: "AddressRenter",
                columns: table => new
                {
                    AddressesAddressId = table.Column<int>(type: "int", nullable: false),
                    RentersRenterID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressRenter", x => new { x.AddressesAddressId, x.RentersRenterID });
                    table.ForeignKey(
                        name: "FK_AddressRenter_Addresses_AddressesAddressId",
                        column: x => x.AddressesAddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressRenter_Renters_RentersRenterID",
                        column: x => x.RentersRenterID,
                        principalTable: "Renters",
                        principalColumn: "RenterID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressRenter_RentersRenterID",
                table: "AddressRenter",
                column: "RentersRenterID");
        }
    }
}
