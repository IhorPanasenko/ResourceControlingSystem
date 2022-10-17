using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResourceControlingAPI.Migrations
{
    public partial class DeviceAddressRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Device_Meters_MeterId",
                table: "Device");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Device",
                table: "Device");

            migrationBuilder.RenameTable(
                name: "Device",
                newName: "Devices");

            migrationBuilder.RenameIndex(
                name: "IX_Device_MeterId",
                table: "Devices",
                newName: "IX_Devices_MeterId");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Devices",
                table: "Devices",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_AddressId",
                table: "Devices",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Addresses_AddressId",
                table: "Devices",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Meters_MeterId",
                table: "Devices",
                column: "MeterId",
                principalTable: "Meters",
                principalColumn: "MeterId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Addresses_AddressId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Meters_MeterId",
                table: "Devices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Devices",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_AddressId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Devices");

            migrationBuilder.RenameTable(
                name: "Devices",
                newName: "Device");

            migrationBuilder.RenameIndex(
                name: "IX_Devices_MeterId",
                table: "Device",
                newName: "IX_Device_MeterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Device",
                table: "Device",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Device_Meters_MeterId",
                table: "Device",
                column: "MeterId",
                principalTable: "Meters",
                principalColumn: "MeterId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
