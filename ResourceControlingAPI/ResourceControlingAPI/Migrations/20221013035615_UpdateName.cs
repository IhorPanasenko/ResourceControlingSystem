using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResourceControlingAPI.Migrations
{
    public partial class UpdateName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_meterReadings_Meters_MeterId",
                table: "meterReadings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_meterReadings",
                table: "meterReadings");

            migrationBuilder.RenameTable(
                name: "meterReadings",
                newName: "MeterReadings");

            migrationBuilder.RenameIndex(
                name: "IX_meterReadings_MeterId",
                table: "MeterReadings",
                newName: "IX_MeterReadings_MeterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeterReadings",
                table: "MeterReadings",
                column: "MeterReadingId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeterReadings_Meters_MeterId",
                table: "MeterReadings",
                column: "MeterId",
                principalTable: "Meters",
                principalColumn: "MeterId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeterReadings_Meters_MeterId",
                table: "MeterReadings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeterReadings",
                table: "MeterReadings");

            migrationBuilder.RenameTable(
                name: "MeterReadings",
                newName: "meterReadings");

            migrationBuilder.RenameIndex(
                name: "IX_MeterReadings_MeterId",
                table: "meterReadings",
                newName: "IX_meterReadings_MeterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_meterReadings",
                table: "meterReadings",
                column: "MeterReadingId");

            migrationBuilder.AddForeignKey(
                name: "FK_meterReadings_Meters_MeterId",
                table: "meterReadings",
                column: "MeterId",
                principalTable: "Meters",
                principalColumn: "MeterId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
